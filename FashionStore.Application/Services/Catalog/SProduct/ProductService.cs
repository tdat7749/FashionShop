using FashionStore.Application.Services.Common;
using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.Data.Enums;
using FashionStore.ViewModel.Catalog.Option;
using FashionStore.ViewModel.Catalog.Product;
using FashionStore.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SProduct
{
    public class ProductService : IProductService
    {
        private readonly FashionStoreDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        public ProductService(FashionStoreDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<ApiResult<bool>> CreateImage(CreateProductImageRequest request)
        {
            var product = await _context.Product.FindAsync(request.ProductId);
            if (product == null) return new ApiFailedResult<bool>($"Sản phẩm với Id = {request.ProductId} không tồn tại");
            if(request.Image != null)
            {
                var listProductImage = new List<ProductImage>();
                foreach (var item in request.Image)
                {
                    var productImage = new ProductImage()
                    {
                        ProductId = request.ProductId,
                        NameImage = request.NameImage,
                        Url = await this.SaveFile(item)
                    };
                    listProductImage.Add(productImage);
                }
                await _context.ProductImages.AddRangeAsync(listProductImage);
                var result = await _context.SaveChangesAsync() > 0;
                return new ApiSuccessResult<bool>(result);
            }
            return new ApiFailedResult<bool>("Thêm hình ảnh thất bại");
        }

        public async Task<ApiResult<bool>> CreateProduct(CreateProductRequest request)
        {
            var listOption = new List<ProductInOption>();

            foreach(var item in request.ProductInOptions)
            {
                var option = new ProductInOption();
                option.OptionId = item;

                listOption.Add(option);
            }
            
            var product = new Product()
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                Status = request.Status,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Stock = 0,
                Price = request.Price,
                PriceSale = request.PriceSale,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                ProductInOptions = listOption
            };

            if(request.Thumbnail != null)
            {
                var pathFile = await this.SaveFile(request.Thumbnail);
                product.Images = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        NameImage = request.Name,
                        Url = pathFile
                    }
                };
                product.Thumbnail = pathFile;
            }

            await _context.Product.AddAsync(product);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteProductImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) return new ApiFailedResult<bool>($"Hình ảnh với Id = {imageId} không tồn tại");

            _context.ProductImages.Remove(productImage);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteProduct(int productId)
        {
            var product = await _context.Product.FindAsync(productId);
            if (product == null) return new ApiFailedResult<bool>($"Sản phẩm với Id = {productId} không tồn tại");

            _context.Product.Remove(product);
            var result =  await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<PagingResultApiBase<List<ProductVm>>> GetProductPaging(PagingProductRequest request)
        {
            var query = from p in _context.Product
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join b in _context.Brands on p.BrandId equals b.Id
                        orderby p.CreatedAt descending
                        select new { p, c, b };

            

            if(request.Keyword != null)
            {
                query =  query.Where(x => x.p.Name.Contains(request.Keyword.Trim().ToLower()));
            }

            if(request.categoryId != null)
            {
                query = query.Where(x => x.p.CategoryId.Equals(request.categoryId));
            }

            if (request.brandId != null)
            {
                query = query.Where(x => x.p.BrandId.Equals(request.brandId));
            }

            int totalRecordAll = await query.CountAsync();

            if (query.Count() > 0)
            {
                query = query.Skip(((request.PageIndex - 1) * request.PageSize)).Take(request.PageSize);
            }



            var listProduct = await query.Select(x => new ProductVm()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Slug = x.p.Slug,
                Thumbnail = x.p.Thumbnail,
                Description = x.p.Description,
                CreatedAt = x.p.CreatedAt.ToString(),
                UpdatedAt = x.p.UpdateAt.ToString(),
                Category = x.c.Name,
                Brand = x.b.Name,
                Status = x.p.Status.ToString(),
                Price = x.p.Price,
                PriceSale = x.p.PriceSale,
                Stock = x.p.Stock
            }).ToListAsync();

            int totalRecord = await query.CountAsync();
            double totalPage = Math.Ceiling((double)totalRecordAll / request.PageSize);

            return new PagingSuccessResultApi<List<ProductVm>>(totalRecordAll,totalPage, totalRecord, request.PageSize,request.PageIndex,listProduct);
        }

        public async Task<PagingResultApiBase<List<ProductVm>>> GetPublicProductPaging(PagingProductRequest request)
        {
            var query = from p in _context.Product
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join b in _context.Brands on p.BrandId equals b.Id
                        where p.Status == Status.Enable && c.Status == Status.Enable && b.Status == Status.Enable
                        orderby p.CreatedAt descending
                        select new { p, c, b };



            if (request.Keyword != null)
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword.Trim().ToLower()));
            }

            if (request.categoryId != null)
            {
                query = query.Where(x => x.p.CategoryId.Equals(request.categoryId));
            }

            if (request.brandId != null)
            {
                query = query.Where(x => x.p.BrandId.Equals(request.brandId));
            }

            int totalRecordAll = await query.CountAsync();

            if (query.Count() > 0)
            {
                query = query.Skip(((request.PageIndex - 1) * request.PageSize)).Take(request.PageSize);
            }



            var listProduct = await query.Select(x => new ProductVm()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Slug = x.p.Slug,
                Thumbnail = x.p.Thumbnail,
                Description = x.p.Description,
                CreatedAt = x.p.CreatedAt.ToString(),
                UpdatedAt = x.p.UpdateAt.ToString(),
                Category = x.c.Name,
                Brand = x.b.Name,
                Status = x.p.Status.ToString(),
                Price = x.p.Price,
                PriceSale = x.p.PriceSale,
                Stock = x.p.Stock
            }).ToListAsync();

            int totalRecord = await query.CountAsync();
            double totalPage = Math.Ceiling((double)totalRecordAll / request.PageSize);

            return new PagingSuccessResultApi<List<ProductVm>>(totalRecordAll, totalPage, totalRecord, request.PageSize, request.PageIndex, listProduct);
        }

        public async Task<ApiResult<ProductDetailVm>> GetProductDetail(int productId)
        {
            var query = from p in _context.Product
                         join c in _context.Categories on p.CategoryId equals c.Id
                         join b in _context.Brands on p.BrandId equals b.Id
                         where p.Id == productId
                         select new { p, c, b };

            if (query.Count() <= 0) return new ApiFailedResult<ProductDetailVm>($"Không có sản phẩm tồn tại với Id = {productId}");

            var listImages = from pi in _context.ProductImages
                             where pi.ProductId == productId
                             select pi;

            var options = from p in _context.Product
                         join po in _context.ProductInOptions on p.Id equals po.ProductId
                         join o in _context.Options on po.OptionId equals o.Id
                         where p.Id == productId
                         select o;

            if (options.Count() <= 0) return new ApiFailedResult<ProductDetailVm>($"Sản phẩm này không có thuộc tính");


            var colors = new List<OptionVm>();
            var sizes = new List<OptionVm>();
            foreach (var item in options)
            {
                if (item.Name.Equals("Color"))
                {
                    var option = new OptionVm();
                    option.Id = item.Id;
                    option.Name = item.Name;
                    option.Value = item.Value;

                    colors.Add(option);
                }
                else if (item.Name.Equals("Size"))
                {
                    var option = new OptionVm();
                    option.Id = item.Id;
                    option.Name = item.Name;
                    option.Value = item.Value;

                    sizes.Add(option);
                }
            }


            var listProduct = await query.Select(x => new ProductDetailVm()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Slug = x.p.Slug,
                Thumbnail = x.p.Thumbnail,
                Description = x.p.Description,
                Category = x.c.Name,
                Brand = x.b.Name,
                Status = x.p.Status.ToString(),
                Price = x.p.Price,
                PriceSale = x.p.PriceSale,
                Stock = x.p.Stock,
                ProductImages = listImages.Select(w => new ProductImageVm()
                {
                    NameImage = w.NameImage,
                    Url = w.Url
                }).ToList(),
                Colors = colors,
                Sizes = sizes,
                CreatedAt = x.p.CreatedAt.ToString(),
                UpdatedAt = x.p.UpdateAt.ToString(),
            }).FirstOrDefaultAsync();


            return new ApiSuccessResult<ProductDetailVm>(listProduct);
        }

        public async Task<ApiResult<ProductDetailVm>> GetProductDetailBySlug(string slug)
        {
            var query = from p in _context.Product
                        join c in _context.Categories on p.CategoryId equals c.Id
                        join b in _context.Brands on p.BrandId equals b.Id
                        where p.Slug.Equals(slug)
                        select new { p, c, b };

            if (query.Count() <= 0) return new ApiFailedResult<ProductDetailVm>($"Không có sản phẩm tồn tại với slug = {slug}");

            var listImages = from p in _context.Product
                             join pi in _context.ProductImages on p.Id equals pi.ProductId
                             where p.Slug.Equals(slug)
                             select pi;

            var options = from p in _context.Product
                          join po in _context.ProductInOptions on p.Id equals po.ProductId
                          join o in _context.Options on po.OptionId equals o.Id
                          where p.Slug.Equals(slug)
                          select o;

            if (options.Count() <= 0) return new ApiFailedResult<ProductDetailVm>($"Sản phẩm này không có thuộc tính");


            var colors = new List<OptionVm>();
            var sizes = new List<OptionVm>();
            foreach (var item in options)
            {
                if (item.Name.Equals("Color"))
                {
                    var option = new OptionVm();
                    option.Id = item.Id;
                    option.Name = item.Name;
                    option.Value = item.Value;

                    colors.Add(option);
                }
                else if (item.Name.Equals("Size"))
                {
                    var option = new OptionVm();
                    option.Id = item.Id;
                    option.Name = item.Name;
                    option.Value = item.Value;

                    sizes.Add(option);
                }
            }


            var listProduct = await query.Select(x => new ProductDetailVm()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Slug = x.p.Slug,
                Thumbnail = x.p.Thumbnail,
                Description = x.p.Description,
                Category = x.c.Name,
                Brand = x.b.Name,
                Status = x.p.Status.ToString(),
                Price = x.p.Price,
                PriceSale = x.p.PriceSale,
                Stock = x.p.Stock,
                ProductImages = listImages.Select(w => new ProductImageVm()
                {
                    NameImage = w.NameImage,
                    Url = w.Url
                }).ToList(),
                Colors = colors,
                Sizes = sizes,
                CreatedAt = x.p.CreatedAt.ToString(),
                UpdatedAt = x.p.UpdateAt.ToString(),
            }).FirstOrDefaultAsync();


            return new ApiSuccessResult<ProductDetailVm>(listProduct);
        }



        public async Task<ApiResult<bool>> UpdatePrice(int productId, int newPrice)
        {
            var product = await _context.Product.FindAsync(productId);
            if (product == null) return new ApiFailedResult<bool>($"Sản phẩm với Id = {productId} không tồn tại");

            if(newPrice < 0)
            {
                return new ApiFailedResult<bool>("Giá tiền của sản phẩm không được < 0");
            }

            product.Price = newPrice;

            _context.Product.Update(product);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> UpdatePriceSale(int productId, int newPriceSale)
        {
            var product = await _context.Product.FindAsync(productId);
            if (product == null) return new ApiFailedResult<bool>($"Sản phẩm với Id = {productId} không tồn tại");

            if (newPriceSale < 0)
            {
                return new ApiFailedResult<bool>("Giá tiền khuyến của sản phẩm không được < 0");
            }

            product.PriceSale = newPriceSale;

            _context.Product.Update(product);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> UpdateProduct(UpdateProductRequest request)
        {
            var product = await _context.Product.FindAsync(request.ProductId);
            if (product == null) return new ApiFailedResult<bool>($"Không có sản phẩm nào với Id = {request.ProductId}");

            product.Price = request.Price;
            product.Name = request.ProductName;
            product.Slug = request.Slug;
            product.PriceSale = request.PriceSale;
            product.Description = request.Description;
            product.Status = request.Status;
            product.CategoryId = request.CategoryId;
            product.BrandId = request.BrandId;

            if (request.Thumbnail != null)
            {
                var pathFile = await this.SaveFile(request.Thumbnail);
                product.Images = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        NameImage = request.ProductName,
                        Url = pathFile
                    }
                };
                product.Thumbnail = pathFile;
            }

            _context.Product.Update(product);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> UpdateStock(UpdateProductStockRequest request)
        {
            var product = await _context.Product.FindAsync(request.ProductId);
            if (product == null) return new ApiFailedResult<bool>($"Sản phẩm với Id = {request.ProductId} không tồn tại");

            if (request.NewStock < 0)
            {
                return new ApiFailedResult<bool>("Số lượng của sản phẩm không được < 0");
            }

            product.Stock = request.NewStock;

            _context.Product.Update(product);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<bool> MinusStock(List<OrderDetail> listOrderDetail)
        {
            foreach (var item in listOrderDetail)
            {
                var product = await _context.Product.FindAsync(item.ProductId);
                product.Stock = product.Stock - item.Quantity;
            }
            return true;
        }

        public async Task<bool> PlusStock(List<OrderDetail> listOrderDetail)
        {
            foreach (var item in listOrderDetail)
            {
                var product = await _context.Product.FindAsync(item.ProductId);
                product.Stock = product.Stock + item.Quantity;
            }
            return true;
        }

        public async Task<ApiResult<List<ProductVm>>> GetLatestProduct()
        {
            var query = (from p in _context.Product
                         join c in _context.Categories on p.CategoryId equals c.Id
                         join b in _context.Brands on p.BrandId equals b.Id
                         orderby p.CreatedAt descending
                         select new { p, c, b }).Take(12);


            var listProduct = await query.Select(x => new ProductVm()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Slug = x.p.Slug,
                Thumbnail = x.p.Thumbnail,
                Description = x.p.Description,
                Category = x.c.Name,
                Brand = x.b.Name,
                Status = x.p.Status.ToString(),
                Price = x.p.Price,
                PriceSale = x.p.PriceSale,
                Stock = x.p.Stock,
                CreatedAt = x.p.CreatedAt.ToString(),
                UpdatedAt = x.p.UpdateAt.ToString(),
            }).ToListAsync();


            return new ApiSuccessResult<List<ProductVm>>(listProduct);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}

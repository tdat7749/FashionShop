using FashionStore.Data.Entities;
using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.Catalog.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SProduct
{
    public interface IProductService
    {

        //ADMIN
        Task<ApiResult<bool>> UpdateStock(UpdateProductStockRequest request);
        Task<ApiResult<bool>> UpdatePrice(int productId, int newPrice);
        Task<ApiResult<bool>> UpdatePriceSale(int productId,int newPriceSale);
        Task<ApiResult<bool>> UpdateStatus(UpdateProductStatusRequest request);
        Task<ApiResult<bool>> DeleteProduct(int productId);
        Task<ApiResult<bool>> DeleteProductImage(int imageId);
        Task<PagingResultApiBase<List<ProductVm>>> GetProductPaging(PagingProductRequest request);
        Task<ApiResult<List<ProductImageVm>>> GetProductImage(int productId);
        Task<ApiResult<bool>> UpdateProduct(UpdateProductRequest request);

        Task<ApiResult<bool>> CreateProduct(CreateProductRequest request);

        Task<ApiResult<bool>> CreateImage(CreateProductImageRequest request);

        Task<bool> MinusStock(List<OrderDetail> listOrderDetail);
        Task<bool> PlusStock(List<OrderDetail> listOrderDetail);

        
        
        //CLIENT

        Task<PagingResultApiBase<List<ProductVm>>> GetPublicProductPaging(PagingProductRequest request);
        Task<ApiResult<ProductDetailVm>> GetProductDetail(int productId);
        Task<ApiResult<ProductDetailVm>> GetProductDetailBySlug(string slug);

        Task<ApiResult<List<ProductVm>>> GetLatestProduct();


    }
}

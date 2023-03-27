using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.Data.Enums;
using FashionStore.ViewModel.Catalog.Brand;
using FashionStore.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SBrand
{
    public class BrandService : IBrandService
    {
        private readonly FashionStoreDbContext _context;
        public BrandService(FashionStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateBrand(CreateBrandRequest request)
        {
            var brand = new Brand()
            {
                Name = request.Name,
                Status = request.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Brands.AddAsync(brand);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteBrand(int brandId)
        {
            var brand = await _context.Brands.FindAsync(brandId);
            if (brand == null) return new ApiFailedResult<bool>($"Không tồn tại danh mục với Id = {brandId}");

            _context.Brands.Remove(brand);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<List<BrandVm>>> GetAllBrands()
        {
            var listBrand = await _context.Brands.Select(x => new BrandVm()
            {
                Name = x.Name,
                Id = x.Id,
                CreatedAt = x.CreatedAt.ToString(),
                UpdatedAt = x.UpdatedAt.ToString(),
                Status = x.Status.ToString()
            }).ToListAsync();

            return new ApiSuccessResult<List<BrandVm>>(listBrand);
        }

        public async Task<ApiResult<List<BrandVm>>> GetPublicBrands()
        {
            var listBrand = await _context.Brands
                .Where(x => x.Status == Status.Enable)
                .Select(x => new BrandVm()
            {
                Name = x.Name,
                Id = x.Id,
                CreatedAt = x.CreatedAt.ToString(),
                UpdatedAt = x.UpdatedAt.ToString(),
                Status = x.Status.ToString()
            }).ToListAsync();

            return new ApiSuccessResult<List<BrandVm>>(listBrand);
        }

        public async Task<ApiResult<BrandVm>> GetDetailBrand(int id)
        {
            var listBrand = await _context.Brands.Where(x => x.Id == id).Select(x => new BrandVm()
            {
                Name = x.Name,
                Id = x.Id,
                CreatedAt = x.CreatedAt.ToString(),
                UpdatedAt = x.UpdatedAt.ToString(),
                Status = x.Status.ToString()
            }).FirstOrDefaultAsync();

            return new ApiSuccessResult<BrandVm>(listBrand);
        }

        public async Task<ApiResult<bool>> UpdateBrand(UpdateBrandRequest request)
        {
            var brand = await _context.Brands.FindAsync(request.Id);
            if (brand == null) return new ApiFailedResult<bool>($"Không tồn tại danh mục với Id = {request.Id}");

            brand.Name = request.Name;
            brand.UpdatedAt = DateTime.Now;
            brand.Status = request.Status;

            _context.Brands.Update(brand);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> UpdateStatusBrand(UpdateStatusBrandRequest request)
        {
            var brand = await _context.Brands.FindAsync(request.Id);
            if (brand == null) return new ApiFailedResult<bool>($"Không tồn tại danh mục với Id = {request.Id}");

            brand.Status = request.Status;
            _context.Brands.Update(brand);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }
    }
}

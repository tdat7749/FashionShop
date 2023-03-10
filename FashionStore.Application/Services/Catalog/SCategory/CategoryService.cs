using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.Data.Enums;
using FashionStore.ViewModel.Catalog.Category;
using FashionStore.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SCategory
{
    public class CategoryService : ICategoryService
    {
        private FashionStoreDbContext _context;
        public CategoryService(FashionStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request)
        {
            var category = new Category()
            {
                Name = request.CategoryName,
                Status = request.Status,
                ParentId = request.ParentId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Categories.AddAsync(category);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return new ApiFailedResult<bool>($"Không tồn tại danh mục với Id = {categoryId}");

             _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);

        }

        public async Task<ApiResult<List<CategoryVm>>> GetAllCategories()
        {
            var listCategory =  await _context.Categories.Select(x => new CategoryVm()
            {
                CategoryName = x.Name,
                Id = x.Id,
                ParentId = x.ParentId,
                CreatedAt = x.CreatedAt.ToString(),
                UpdatedAt = x.UpdatedAt.ToString(),
                Status = x.Status.ToString(),
            }).ToListAsync();

            return new ApiSuccessResult<List<CategoryVm>>(listCategory);
        }

        public async Task<ApiResult<bool>> UpdateCategory(UpdateCategoryRequest request)
        {
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category == null) return new ApiFailedResult<bool>($"Không tồn tại danh mục với Id = {request.CategoryId}");

            category.Name = request.CategoryName;
            category.ParentId = request.ParentId;
            category.UpdatedAt = DateTime.Now;
            category.Status = request.Status;

            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);


        }

        public async Task<ApiResult<bool>> UpdateStatusCategory(UpdateStatusCategoryRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null) return new ApiFailedResult<bool>($"Không tồn tại danh mục với Id = {request.Id}");

            category.Status = request.Status;
            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<List<CategoryVm>>> GetCategoriesByParentId(int parentId)
        {
            var listCategory = await _context.Categories
                .Where(x => x.ParentId == parentId && x.Status == Status.Enable)
                .Select(x => new CategoryVm()
                {
                    CategoryName = x.Name,
                    Id = x.Id,
                    ParentId = (int)x.ParentId,
                    CreatedAt = x.CreatedAt.ToString(),
                    UpdatedAt = x.UpdatedAt.ToString(),
                    Status = x.Status.ToString(),
                }).ToListAsync();

            return new ApiSuccessResult<List<CategoryVm>>(listCategory);
        }

        public async Task<ApiResult<List<CategoryVm>>> GetCategoriesLow()
        {
            var listCategory = await _context.Categories
                .Where(x => x.ParentId != 0)
                .Select(x => new CategoryVm()
                {
                    CategoryName = x.Name,
                    Id = x.Id,
                    ParentId = (int)x.ParentId,
                    CreatedAt = x.CreatedAt.ToString(),
                    UpdatedAt = x.UpdatedAt.ToString(),
                    Status = x.Status.ToString(),
                }).ToListAsync();

            return new ApiSuccessResult<List<CategoryVm>>(listCategory);
        }

        public async Task<ApiResult<CategoryVm>> GetDetailCategory(int id)
        {
            var category = await _context.Categories
                .Where(x => x.Id == id)
                .Select(x => new CategoryVm()
                {
                    CategoryName = x.Name,
                    Id = x.Id,
                    ParentId = (int)x.ParentId,
                    CreatedAt = x.CreatedAt.ToString(),
                    UpdatedAt = x.UpdatedAt.ToString(),
                    Status = x.Status.ToString(),
                }).FirstOrDefaultAsync();

            return new ApiSuccessResult<CategoryVm>(category);
        }

        public async Task<ApiResult<List<CategoryVm>>> GetParentCategories()
        {
            var listCategory = await _context.Categories
                .Where(x => x.ParentId == 0)
                .Select(x => new CategoryVm()
                {
                    CategoryName = x.Name,
                    Id = x.Id,
                    ParentId = (int)x.ParentId,
                    CreatedAt = x.CreatedAt.ToString(),
                    UpdatedAt = x.UpdatedAt.ToString(),
                    Status = x.Status.ToString(),
                }).ToListAsync();

            return new ApiSuccessResult<List<CategoryVm>>(listCategory);
        }


        public async Task<ApiResult<List<CategoryVm>>> GetPublicParentCategories()
        {
            var listCategory = await _context.Categories
                .Where(x => x.Status == Status.Enable && x.ParentId == 0)
                .Select(x => new CategoryVm()
                {
                    CategoryName = x.Name,
                    Id = x.Id,
                    ParentId = (int)x.ParentId,
                    CreatedAt = x.CreatedAt.ToString(),
                    UpdatedAt = x.UpdatedAt.ToString(),
                    Status = x.Status.ToString(),
                }).ToListAsync();

            return new ApiSuccessResult<List<CategoryVm>>(listCategory);
        }
    }
}

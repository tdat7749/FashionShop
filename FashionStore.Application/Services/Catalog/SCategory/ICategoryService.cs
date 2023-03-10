using FashionStore.Data.Enums;
using FashionStore.ViewModel.Catalog.Category;
using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SCategory
{
    public interface ICategoryService
    {

        //ADMIN
        Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request);
        Task<ApiResult<bool>> UpdateCategory(UpdateCategoryRequest request);
        Task<ApiResult<bool>> DeleteCategory(int categoryId);
        Task<ApiResult<bool>> UpdateStatusCategory(UpdateStatusCategoryRequest request);

        Task<ApiResult<List<CategoryVm>>> GetCategoriesLow();
        Task<ApiResult<List<CategoryVm>>> GetAllCategories();
        Task<ApiResult<List<CategoryVm>>> GetParentCategories();

        Task<ApiResult<CategoryVm>> GetDetailCategory(int id);

        //Client

        Task<ApiResult<List<CategoryVm>>> GetPublicParentCategories();

        Task<ApiResult<List<CategoryVm>>> GetCategoriesByParentId(int parentId);
    }

}

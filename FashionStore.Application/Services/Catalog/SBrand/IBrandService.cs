using FashionStore.Data.Enums;
using FashionStore.ViewModel.Catalog.Brand;
using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SBrand
{
    public interface IBrandService
    {
        //ADMIN
        Task<ApiResult<bool>> CreateBrand(CreateBrandRequest request);
        Task<ApiResult<bool>> UpdateBrand(UpdateBrandRequest request);
        Task<ApiResult<bool>> DeleteBrand(int brandId);
        Task<ApiResult<bool>> UpdateStatusBrand(UpdateStatusBrandRequest request);
        Task<ApiResult<List<BrandVm>>> GetAllBrands();
        Task<ApiResult<BrandVm>> GetDetailBrand(int id);


        //CLIENT
        Task<ApiResult<List<BrandVm>>> GetPublicBrands();
    }
}

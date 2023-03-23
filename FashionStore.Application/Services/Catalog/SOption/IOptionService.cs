using FashionStore.ViewModel.Catalog.Option;
using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SOption
{
    public interface IOptionService
    {
        Task<ApiResult<List<OptionVm>>> GetColorOption();
        Task<ApiResult<List<OptionVm>>> GetAllOption();
        Task<ApiResult<List<OptionVm>>> GetSizeOption();
        Task<ApiResult<bool>> CreateOption(CreateOptionRequest request);
        Task<ApiResult<bool>> UpdateOption(UpdateOptionRequest request);

        Task<ApiResult<bool>> DeleteOption(int id);
    }
}

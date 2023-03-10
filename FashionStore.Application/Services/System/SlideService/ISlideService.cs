using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.System.SlideService
{
    public interface ISlideService
    {
        //ADMIN
        Task<ApiResult<bool>> CreateSlide(CreateSlideRequest request);
        Task<ApiResult<bool>> UpdateSlide(UpdateSlideRequest request);
        Task<ApiResult<bool>> UpdateStatusSlide(UpdateStatusSlideRequest request);
        Task<ApiResult<bool>> DeleteSlide(int slideId);
        Task<ApiResult<List<SlideVm>>> GetAllSlides();

        //ADMIN
        Task<ApiResult<List<SlideVm>>> GetAllSlidesEnable();
    }
}

using FashionStore.Application.Services.Common;
using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.Data.Enums;
using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.Slide;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.System.SlideService
{
    public class SlideService : ISlideService
    {
        private readonly FashionStoreDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        public SlideService(FashionStoreDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<ApiResult<bool>> CreateSlide(CreateSlideRequest request)
        {
            var slide = new Slide()
            {
                CreatedAt = DateTime.Now,
                NameImage = request.Name,
                SortOrder = request.SortOrder,
                Status = request.Status
            };

            if(request.Url != null)
            {
                slide.Url = await this.SaveFile(request.Url);
            }
            else
            {
                return new ApiFailedResult<bool>("Vui lòng thêm ảnh !");
            }

            _context.Slides.Add(slide);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteSlide(int slideId)
        {
            var slide = await _context.Slides.FindAsync(slideId);
            if (slide == null) return new ApiFailedResult<bool>($"Không tồn tại slide với Id = {slideId}");

            _context.Slides.Remove(slide);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<SlideVm>> GetDetailSlide(int slideId)
        {
            var slide = await _context.Slides.FindAsync(slideId);
            if (slide == null) return new ApiFailedResult<SlideVm>($"Không tồn tại slide với Id = {slideId}");

            var slideVm = new SlideVm();
            slideVm.Id = slide.Id;
            slideVm.Url = slide.Url;
            slideVm.SortOrder = slide.SortOrder;
            slideVm.Status = slide.Status.ToString();
            slideVm.Name = slide.NameImage;
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<SlideVm>(slideVm);
        }

        public async Task<ApiResult<List<SlideVm>>> GetAllSlides()
        {
            var query = from s in _context.Slides
                         select s;

            var slides = await query.Select(x => new SlideVm()
            {
                Id = x.Id,
                Name = x.NameImage,
                Url = x.Url,
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt.ToString(),
                SortOrder = x.SortOrder
            }).ToListAsync();

            return new ApiSuccessResult<List<SlideVm>>(slides);

        }

        public async Task<ApiResult<List<SlideVm>>> GetAllSlidesEnable()
        {
            var query = from s in _context.Slides
                        where s.Status == Status.Enable
                        select s;

            var slides = await query.Select(x => new SlideVm()
            {
                Id = x.Id,
                Name = x.NameImage,
                Url = x.Url,
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt.ToString(),
                SortOrder = x.SortOrder
            }).OrderBy(x => x.SortOrder).ToListAsync();

            return new ApiSuccessResult<List<SlideVm>>(slides);

        }

        public async Task<ApiResult<bool>> UpdateSlide(UpdateSlideRequest request)
        {
            var slide = await _context.Slides.FindAsync(request.SlideId);
            if (slide == null) return new ApiFailedResult<bool>($"Không tồn tại slide với Id = {request.SlideId}");

            slide.SortOrder = request.SortOrder;
            slide.NameImage = request.Name;
            slide.Status = request.Status;

            if(request.Url != null)
            {
                slide.Url = await SaveFile(request.Url);
            }

            _context.Slides.Update(slide);
            var result = await _context.SaveChangesAsync() > 0;

            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> UpdateStatusSlide(UpdateStatusSlideRequest request)
        {
            var slide = await _context.Slides.FindAsync(request.SlideId);
            if (slide == null) return new ApiFailedResult<bool>($"Không tồn tại slide với Id = {request.SlideId}");

            slide.Status = request.Status;

            _context.Slides.Update(slide);
            var result = await _context.SaveChangesAsync() > 0;

            return new ApiSuccessResult<bool>(result);
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

using FashionStore.Application.Services.System.SlideService;
using FashionStore.ViewModel.System.Slide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SlidesController : ControllerBase
    {
        private readonly ISlideService _slideService;
        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }

        [HttpGet]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetAllSlides()
        {
            var result = await _slideService.GetAllSlides();
            if(result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("enable")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSlidesEnable()
        {
            var result = await _slideService.GetAllSlidesEnable();
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetDetailSlide(int id)
        {
            var result = await _slideService.GetDetailSlide(id);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateSlide([FromForm]UpdateSlideRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _slideService.UpdateSlide(request);
            return Ok(result);
        }

        [HttpPut("status")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateStatusSlide([FromBody]UpdateStatusSlideRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _slideService.UpdateStatusSlide(request);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> CreateSlide([FromForm] CreateSlideRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _slideService.CreateSlide(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> DeleteSlide(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _slideService.DeleteSlide(id);
            return Ok(result);
        }
    }
}

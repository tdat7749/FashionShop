using FashionStore.Application.Services.Catalog.SOption;
using FashionStore.ViewModel.Catalog.Option;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionService _optionService;
        public OptionsController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet("color")]
        [AllowAnonymous]
        public async Task<IActionResult> GetColorOption()
        {
            var option = await _optionService.GetColorOption();
            if (option == null)
            {
                return BadRequest();
            }
            return Ok(option);
        }

        [HttpGet("size")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSizeOption()
        {
            var option = await _optionService.GetSizeOption();
            if (option == null)
            {
                return BadRequest();
            }
            return Ok(option);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllOption()
        {
            var option = await _optionService.GetAllOption();
            if (option == null)
            {
                return BadRequest();
            }
            return Ok(option);
        }


        [HttpPost]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> CreateOption(CreateOptionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _optionService.CreateOption(request);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateOption(UpdateOptionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _optionService.UpdateOption(request);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _optionService.DeleteOption(id);
            return Ok(result);
        }
    }
}

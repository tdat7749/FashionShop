using FashionStore.Application.Services.Catalog.SBrand;
using FashionStore.ViewModel.Catalog.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrands();
            if(brands == null)
            {
                return BadRequest();
            }
            return Ok(brands);
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicBrands()
        {
            var brands = await _brandService.GetPublicBrands();
            if (brands == null)
            {
                return BadRequest();
            }
            return Ok(brands);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetDetailBrand(int id)
        {
            var brand = await _brandService.GetDetailBrand(id);
            if (brand == null)
            {
                return BadRequest();
            }
            return Ok(brand);
        }

        [HttpPost]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> CreateBrand([FromBody]CreateBrandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _brandService.CreateBrand(request);
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateBrand([FromBody]UpdateBrandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _brandService.UpdateBrand(request);
            return Ok(result);
        }

        [HttpPatch("status")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateStatusBrand([FromBody] UpdateStatusBrandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _brandService.UpdateStatusBrand(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _brandService.DeleteBrand(id);
            return Ok(result);
        }
    }
}

using FashionStore.Application.Services.Catalog.SCategory;
using FashionStore.ViewModel.Catalog.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            if (categories == null)
            {
                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpGet("parent")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetParentCategories()
        {
            var categories = await _categoryService.GetParentCategories();
            if (categories == null)
            {
                return BadRequest();
            }
            return Ok(categories);
        }


        [HttpGet("parent/{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetCategoriesByParentId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categories = await _categoryService.GetCategoriesByParentId(id);
            if (categories == null)
            {
                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpGet("low")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoriesLow()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categories = await _categoryService.GetCategoriesLow();
            if (categories == null)
            {
                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetDetailCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = await _categoryService.GetDetailCategory(id);
            if (category== null)
            {
                return BadRequest();
            }
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _categoryService.CreateCategory(request);
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _categoryService.UpdateCategory(request);
            return Ok(result);
        }

        [HttpPatch("status")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateStatusCategory([FromBody] UpdateStatusCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _categoryService.UpdateStatusCategory(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _categoryService.DeleteCategory(id);
            return Ok(result);
        }
    }
}

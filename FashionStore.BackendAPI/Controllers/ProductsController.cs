using FashionStore.Application.Services.Catalog.SProduct;
using FashionStore.ViewModel.Catalog.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("paging")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetProductPaging([FromQuery]PagingProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.GetProductPaging(request);
            return Ok(result);
        }

        [HttpGet("images/{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetProductImage(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.GetProductImage(id);
            return Ok(result);
        }

        [HttpGet("public/paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicProductPaging([FromQuery] PagingProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.GetPublicProductPaging(request);
            return Ok(result);
        }

        [HttpGet("latest")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestProduct()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.GetLatestProduct();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.GetProductDetail(id);
            return Ok(result);
        }

        [HttpGet("slug/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductDetailBySlug(string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.GetProductDetailBySlug(slug);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Quản Trị Viên")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.CreateProduct(request);
            return Ok(result);
        }

        [HttpPost("images")]
        [Authorize(Roles = "Quản Trị Viên")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProductImage([FromForm] CreateProductImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.CreateImage(request);
            return Ok(result);
        }

        [HttpPatch("stock")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateStock([FromBody]UpdateProductStockRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateStock(request);
            return Ok(result);
        }



        [HttpPatch("status")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateStatus(UpdateProductStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateStatus(request);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Quản Trị Viên")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateProduct(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }


        [HttpDelete("image/{id}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.DeleteProductImage(id);
            return Ok(result);
        }
    }
}

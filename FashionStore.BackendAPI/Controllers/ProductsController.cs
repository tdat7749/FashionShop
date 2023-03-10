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
        [AllowAnonymous]
        public async Task<IActionResult> GetProductPaging([FromQuery]PagingProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.GetProductPaging(request);
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
        //[Authorize(Roles = "Quản Trị Viên")]
        [AllowAnonymous]
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
        // [Authorize(Roles = "Quản Trị Viên")]
        [AllowAnonymous]
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

        [HttpPut("stock")]
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

        [HttpPut]
        [Authorize(Roles = "Quản Trị Viên")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateProduct(request);
            return Ok(result);
        }
    }
}

using FashionStore.Application.Services.Catalog.SOrder;
using FashionStore.ViewModel.Catalog.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Khách Hàng,Quản Trị Viên")]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderService.CreateOrder(request);
            return Ok(result);
        }


        [HttpPut("status")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> ChangeStatusOrder([FromBody] ChangeStatusOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderService.ChangeStatusOrder(request);
            return Ok(result);
        }

        [HttpGet("paging")]
        [Authorize(Roles = "Khách Hàng,Quản Trị Viên")]
        public async Task<IActionResult> GetListOrderById([FromQuery]PagingOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _orderService.GetListOrdersById(request);

            return Ok(result);
        }
    }
}

using FashionStore.Application.Services.System;
using FashionStore.ViewModel.System.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticatesController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticatesController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authenticateService.Register(request);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authenticateService.Authenticate(request);
            return Ok(result);
        }

        [HttpGet("verify/{id}")]
        [Authorize(Roles = "Quản Trị Viên,Khách Hàng")]
        public async Task<IActionResult> VerifyClient(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authenticateService.VerifyClient(id);
            return Ok(result);
        }
    }
}

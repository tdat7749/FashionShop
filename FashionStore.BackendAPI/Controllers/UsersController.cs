using FashionStore.Application.Services.System.UserService;
using FashionStore.ViewModel.System.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("paging")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> GetPagingUsers([FromQuery]PagingUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.GetPagingUsers(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Quản Trị Viên,Khách Hàng")]
        public async Task<IActionResult> GetDetailUser(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.GetDetailUser(id);
            return Ok(result);
        }

        [HttpGet("role")]
        //[Authorize(Roles = "Quản Trị Viên")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRoles()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userService.GetAllRoles();
            return Ok(result);
        }

        [HttpGet("role/{userName}")]
        //[Authorize(Roles = "Quản Trị Viên")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoleUser(string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userService.GetRoleUser(userName);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Quản Trị Viên,Khách Hàng")]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUser(request);
            return Ok(result);
        }

        [HttpPut("roles")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> UpdateRolesUser([FromBody] UpdateRolesUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateRolesUser(request);
            return Ok(result);
        }


        [HttpPatch("password")]
        //[Authorize(Roles = "Khách Hàng,Quản Trị Viên")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.ChangePassword(request);
            return Ok(result);
        }

        [HttpDelete("{userName}")]
        [Authorize(Roles = "Quản Trị Viên")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.DeleteUser(userName);
            return Ok(result);
        }

    }
}

using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using FashionStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FashionStore.Data.EF;

namespace FashionStore.Application.Services.System
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly FashionStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,FashionStoreDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<ApiResult<LoginResponseVm>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiFailedResult<LoginResponseVm>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password,false,true);

            if (!result.Succeeded) return new ApiFailedResult<LoginResponseVm>("Sai mật khẩu");

            var userRoles = await _userManager.GetRolesAsync(user);

            var listClaim = new List<Claim>()
            {
                new Claim("UserId",user.Id)
            };

            foreach (var role in userRoles)
            {
                listClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var rawToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: listClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );


            var token = new JwtSecurityTokenHandler().WriteToken(rawToken);

            var response = new LoginResponseVm()
            {
                AccessToken = token,
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return new ApiSuccessResult<LoginResponseVm>(response);

        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null) return new ApiFailedResult<bool>("Tên tài khoản này đã tồn tại");

            var checkEmail = await _userManager.FindByEmailAsync(request.Email);
            if (checkEmail != null) return new ApiFailedResult<bool>("Email này đã tồn tại");

            ApplicationUser newUser = new ApplicationUser();
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.PhoneNumber = request.PhoneNumber;
            newUser.UserName = request.UserName;
            newUser.Email = request.Email;

            var result = await _userManager.CreateAsync(newUser, request.Password);

            await _userManager.AddToRoleAsync(newUser, "Khách Hàng");
            if(result.Succeeded) return new ApiSuccessResult<bool>(true);

            return new ApiFailedResult<bool>("Tạo tài khoản thất bại");
        }

        public async Task<ApiResult<bool>> RegisterAdmin(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null) return new ApiFailedResult<bool>("Tên tài khoản này đã tồn tại");

            var checkEmail = await _userManager.FindByEmailAsync(request.Email);
            if (checkEmail != null) return new ApiFailedResult<bool>("Email này đã tồn tại");

            ApplicationUser newUser = new ApplicationUser();
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.PhoneNumber = request.PhoneNumber;
            newUser.UserName = request.UserName;
            newUser.Email = request.Email;

            var result = await _userManager.CreateAsync(newUser, request.Password);


            await _userManager.AddToRoleAsync(newUser,"Quản Trị Viên");
            if (result.Succeeded) return new ApiSuccessResult<bool>(true);

            return new ApiFailedResult<bool>("Tạo tài khoản thất bại");
        }

        public async Task<ApiResult<UserVm>> VerifyClient(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new ApiFailedResult<UserVm>($"Không tồn người dùng nào có Id =  {userId}");

            var listRoles = await _userManager.GetRolesAsync(user);

            var result = new UserVm()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ListRoles = listRoles.ToList()
            };

            return new ApiSuccessResult<UserVm>(result);
        }
    }
}

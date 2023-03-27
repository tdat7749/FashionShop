using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.System.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly FashionStoreDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, FashionStoreDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<ApiResult<bool>> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return new ApiFailedResult<bool>($"Không tồn người dùng nào có Id =  {request.UserId}");

            if(request.NewPassword != request.ComfirmPassword)
            {
                return new ApiFailedResult<bool>("Mật khẩu mới và mật khẩu xác nhận không trùng khớp !");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ApiFailedResult<bool>("Mật khẩu cũ không chính xác !");
            }

            return new ApiSuccessResult<bool>(result.Succeeded);
        }

        public async Task<ApiResult<UserVm>> GetDetailUser(string userId)
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

        public async Task<ApiResult<List<RoleVm>>> GetAllRoles()
        {
            var listRolesRaw = _roleManager.Roles;

            var listRoles = await listRolesRaw.Select(x => new RoleVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<RoleVm>>(listRoles);
        }

        public async Task<PagingResultApiBase<List<UserVm>>> GetPagingUsers(PagingUserRequest request)
        {
            var query = from u in _context.Users
                           select u;

            if (request.KeyWord != null)
            {
                query = query
                    .Where(x => x.UserName.Contains(request.KeyWord.Trim().ToLower()) || x.PhoneNumber.Contains(request.KeyWord.Trim()));
            }

            int totalRecordAll = await query.CountAsync();

            if (await query.CountAsync() > 0)
            {
                query = query.Skip(((request.PageIndex - 1) * request.PageSize)).Take(request.PageSize);
            }

            var listUsers = await query.Select(x => new UserVm()
            {
                Id = x.Id,
                UserName = x.UserName,
                FirstName= x.FirstName,
                LastName =x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToListAsync();

            int totalRecord = await query.CountAsync();
            double totalPage = Math.Ceiling((double)totalRecordAll / request.PageSize);

            return new PagingSuccessResultApi<List<UserVm>>
                (totalRecordAll,totalPage, totalRecord, request.PageSize, request.PageIndex, listUsers);
        }

        public async Task<ApiResult<bool>> UpdateRolesUser(UpdateRolesUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.userName);
            if (user == null) return new ApiFailedResult<bool>($"Không tồn người dùng nào có tài khoản = {request.userName}");

            var currentRoles = await _userManager.GetRolesAsync(user);


            //add role
            foreach (var role in request.ListRoles)
            {
                if (currentRoles.Contains(role))
                {
                    continue;
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            // remove role
            foreach (var role in currentRoles)
            {
                if (request.ListRoles.Contains(role))
                {
                    continue;
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            return new ApiSuccessResult<bool>(true);

        }

        public async Task<ApiResult<bool>> UpdateUser(UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return new ApiFailedResult<bool>($"Không tồn người dùng nào có Id = {request.UserId}");

            var checkEmail = await _userManager.FindByEmailAsync(request.Email);
            if (checkEmail != null) return new ApiFailedResult<bool>($"Địa chỉ Email này đã tồn tại");

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<IList<string>>> GetRoleUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new ApiFailedResult<IList<string>>($"Không tồn tại người dùng có tài khoản là : ${userName}");

            var listCurrentRoles = await _userManager.GetRolesAsync(user);

            return new ApiSuccessResult<IList<string>>(listCurrentRoles);

        }

        public async Task<ApiResult<bool>> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new ApiFailedResult<bool>($"Không tồn tại người dùng có tài khoản là : ${userName}");

            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);

        }
    }
}

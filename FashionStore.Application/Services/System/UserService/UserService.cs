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
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return new ApiFailedResult<bool>($"Không tồn người dùng nào có Id = {request.UserId}");



            var RemovedRole = request.SelectItems.Where(x => x.IsSelected == false).Select(x => x.Name).ToList();
            foreach(var role in RemovedRole)
            {
                if(await _userManager.IsInRoleAsync(user, role) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            var AddedRole = request.SelectItems.Where(x => x.IsSelected == true).Select(x => x.Name).ToList();
            foreach (var role in AddedRole)
            {
                if (await _userManager.IsInRoleAsync(user, role) == false)
                {
                    await _userManager.AddToRoleAsync(user, role);
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
    }
}

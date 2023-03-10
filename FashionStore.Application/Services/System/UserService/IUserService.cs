using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.System.UserService
{
    public interface IUserService
    {
        //ADMIN
        Task<ApiResult<bool>> UpdateUser(UpdateUserRequest request);
        Task<ApiResult<bool>> UpdateRolesUser(UpdateRolesUserRequest request);
        Task<PagingResultApiBase<List<UserVm>>> GetPagingUsers(PagingUserRequest request);

        //Client đã đăng nhập 
        Task<ApiResult<UserVm>> GetDetailUser(string userId);
    }
}

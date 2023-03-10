using FashionStore.ViewModel.Common;
using FashionStore.ViewModel.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.System
{
    public interface IAuthenticateService
    {
        Task<ApiResult<LoginResponseVm>> Authenticate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> RegisterAdmin(RegisterRequest request);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public T Data { get; set; }
        public ApiSuccessResult()
        {
            Success = true;
            Message = "Request Thành công";
        }

        public ApiSuccessResult(T data)
        {
            Success = true;
            Message = "Request thành công !";
            Data = data;
        }
    }
}

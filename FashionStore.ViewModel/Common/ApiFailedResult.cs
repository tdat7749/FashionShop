using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Common
{
    public class ApiFailedResult<T> : ApiResult<T>
    {
        public ApiFailedResult()
        {
            Success = false;
        }

        public ApiFailedResult(string message)
        {
            Success = false;
            Message = message;
        }
    }
}

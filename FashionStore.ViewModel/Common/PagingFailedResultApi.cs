using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Common
{
    public class PagingFailedResultApi<T> : PagingResultApiBase<T>
    {
        public PagingFailedResultApi(string message)
        {
            Message = message;
            Success = false;
        }
    }
}

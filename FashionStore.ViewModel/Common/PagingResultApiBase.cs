using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Common
{
    public class PagingResultApiBase<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public string Message { get; set; }
        public bool Success { get; set; }

        public PagingResultApiBase()
        {

        }
    }
}

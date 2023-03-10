using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.User
{
    public class PagingUserRequest : PagingBase
    {
        public string KeyWord { get; set; }
    }
}

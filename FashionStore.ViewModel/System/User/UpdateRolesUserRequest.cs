using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.User
{
    public class UpdateRolesUserRequest
    {
        public string UserId { get; set; }
        public List<SelectItem> SelectItems { get; set; }
    }
}

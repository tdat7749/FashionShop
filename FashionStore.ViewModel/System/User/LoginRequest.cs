using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.User
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Password { get; set; }
    }
}

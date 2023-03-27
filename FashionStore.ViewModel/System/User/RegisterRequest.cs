using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.User
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        [MinLength(6, ErrorMessage = "Tối thiểu cần 6 kí tự")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [MinLength(6, ErrorMessage = "Tối thiểu cần 6 kí tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(11, ErrorMessage = "Không được vượt quá 11 kí tự")]

        public string PhoneNumber { get; set; }

    }
}

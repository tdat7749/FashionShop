using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.User
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Tối đa 255 kí tự")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Tối đa 255 kí tự")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Tối đa 255 kí tự")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string PhoneNumber { get; set; }
    }
}

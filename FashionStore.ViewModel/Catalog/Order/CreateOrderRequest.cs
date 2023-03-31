using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Order
{
    public class CreateOrderRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống mã khách hàng")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public double Total { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string FullName { get; set; }
        public string Note { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public List<CreateDetailOrderRequest> Details { get; set; }
    }
}

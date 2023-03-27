using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Order
{
    public class ChangeStatusOrderRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public OrderStatus StatusId { get; set; }
    }
}

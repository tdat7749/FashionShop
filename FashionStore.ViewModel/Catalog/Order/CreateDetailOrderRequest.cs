using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Order
{
    public class CreateDetailOrderRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống mã sản phẩm")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống số lượng")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống giá tiền")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống tổng tiền")]
        public double SubTotal { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống màu sắc")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống kích thước")]
        public string Size { get; set; }
    }
}

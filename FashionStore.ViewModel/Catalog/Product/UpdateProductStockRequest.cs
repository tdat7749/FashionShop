using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Product
{
    public class UpdateProductStockRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int NewStock { get; set; }
    }
}

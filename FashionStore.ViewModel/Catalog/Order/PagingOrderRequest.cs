using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Order
{
    public class PagingOrderRequest : PagingBase
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string UserId { get; set; }
    }
}

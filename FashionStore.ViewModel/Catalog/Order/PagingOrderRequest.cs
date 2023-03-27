using FashionStore.Data.Enums;
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
        public string? UserId { get; set; }
        public int? Status { get; set; }
    }
}

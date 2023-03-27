using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Order
{
    public class DetailOrderVm
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Thumbnail { get; set; }
        public double SubTotal { get; set; }
        public int OrderId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}

using FashionStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Order
{
    public class OrderVm
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string CreatedAt { get; set; }
        public string FullName { get; set; }
        public string Note { get; set; }


        public List<DetailOrderVm> OrderDetails { get; set; }
    }
}

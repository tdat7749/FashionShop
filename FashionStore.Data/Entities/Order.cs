using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Note { get; set; }
        public string FullName { get; set; }


        public List<OrderDetail> OrderDetails { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

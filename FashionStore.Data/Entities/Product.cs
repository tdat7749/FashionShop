using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int Stock { get; set; }
        public double Price { get; set; }
        public double PriceSale { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<ProductImage> Images { get; set; }

        public List<ProductInOption> ProductInOptions { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<Comment> Comments { get; set; }
    }
}

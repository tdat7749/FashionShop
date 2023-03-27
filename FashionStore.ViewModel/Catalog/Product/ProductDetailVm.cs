using FashionStore.Data.Entities;
using FashionStore.ViewModel.Catalog.Brand;
using FashionStore.ViewModel.Catalog.Category;
using FashionStore.ViewModel.Catalog.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Product
{
    public class ProductDetailVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public double Price { get; set; }
        public double PriceSale { get; set; }
        public int Stock { get; set; }

        public ProductCategoryVm Category { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public ProductBrandVm Brand { get; set; }

        public List<ProductImageVm> ProductImages { get; set; }
        public List<OptionVm> Sizes { get; set; }
        public List<OptionVm> Colors { get; set; }
    }
}

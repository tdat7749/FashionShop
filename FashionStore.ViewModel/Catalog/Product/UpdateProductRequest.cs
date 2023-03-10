using FashionStore.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Product
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public Status Status { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int BrandId { get; set; }

        public IFormFile Thumbnail { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public double PriceSale { get; set; }

    }
}

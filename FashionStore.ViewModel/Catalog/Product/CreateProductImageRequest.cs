using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Product
{
    public class CreateProductImageRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string NameImage { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public List<IFormFile> Image { get; set; }
    }
}

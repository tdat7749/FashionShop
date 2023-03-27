using FashionStore.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.Slide
{
    public class UpdateSlideRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int SlideId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string Name { get; set; }
        public IFormFile Url { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int SortOrder { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public Status Status { get; set; }
    }
}

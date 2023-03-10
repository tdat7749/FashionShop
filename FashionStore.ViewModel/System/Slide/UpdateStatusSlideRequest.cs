using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.Slide
{
    public class UpdateStatusSlideRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int SlideId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public Status Status { get; set; }
    }
}

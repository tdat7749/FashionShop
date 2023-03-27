using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.Comment
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Value { get; set; }
    }
}

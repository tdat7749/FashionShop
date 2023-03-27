using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.System.Comment
{
    public class UpdateCommentRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int CommentId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string NewValue { get; set; }
    }
}

using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Category
{
    public class UpdateCategoryRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int ParentId { get; set; }

        public Status Status { get; set; }
    }
}

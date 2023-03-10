using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Category
{
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(255, ErrorMessage = "Không được vượt quá 255 kí tự")]
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public Status Status { get; set; }

    }
}

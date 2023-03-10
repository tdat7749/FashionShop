using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Brand
{
    public class UpdateStatusBrandRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public Status Status { get; set; }
    }
}

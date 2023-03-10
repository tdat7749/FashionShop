using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Category
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Status { get; set; }
    }
}

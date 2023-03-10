using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Brand
{
    public class BrandVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}

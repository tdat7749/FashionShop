using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Common
{
    public class SelectItem
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool IsSelected { get; set; }
    }
}

using FashionStore.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModel.Catalog.Product
{
    public class PagingProductRequest : PagingBase
    {
        public string Keyword { get; set; }
        public int? categoryId { get; set; }
        public int? brandId { get; set; }
    }
}

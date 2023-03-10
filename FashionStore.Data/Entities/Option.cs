using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Data.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }

        public List<ProductInOption> ProductInOptions { get; set; }
    }
}

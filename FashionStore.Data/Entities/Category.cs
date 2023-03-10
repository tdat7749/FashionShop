﻿using FashionStore.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int ParentId { get; set; }
        public Status Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<Product> Products { get; set; }

    }
}

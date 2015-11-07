﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Model
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        [Key]
        public int CategoryId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Name { get; set; }
    }
}

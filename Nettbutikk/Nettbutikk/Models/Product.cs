using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    
    public partial class Product
    {

        public Product()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }
        
        public virtual Category Category { get; set; }
    }
}

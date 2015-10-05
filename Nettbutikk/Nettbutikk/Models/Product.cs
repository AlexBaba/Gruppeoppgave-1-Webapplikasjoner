using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    
    public partial class Product
    {

        public Product(string name, float price, string description, Category category)
        {
            this.Name = name;
            this.Price = price;
            this.Description = description;
            Category = category;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        
        public virtual Category Category { get; set; }
    }
}


namespace Nettbutikk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Category
    {   
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public Category(string name, string description, Category parentCategory = null)
            : this()
        {
            Name = name;
            Description = description;
            ParentCategory = parentCategory;
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public string Name
        {
            get;
            set;
        }

        [Required]
        public string Description
        {
            get;
            set;
        }
        
        public virtual Category ParentCategory { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
        
    }
}

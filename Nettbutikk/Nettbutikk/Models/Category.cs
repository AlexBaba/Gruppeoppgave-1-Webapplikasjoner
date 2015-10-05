
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

        internal string ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}

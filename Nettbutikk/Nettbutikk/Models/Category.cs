using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nettbutikk.Models
{   
    public partial class Category
    {
        private string name;

        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [Required]
        public string Description
        {
            get;
            set;
        }
        
        public virtual Category ParentCategory { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

        public new string ToString()
        {
            return "<" + GetType().FullName + " Name=\"" + Name + "\"," + " Description=\"" + Description + "\", " + " ParentCategory=\"" + ParentCategory.Name + "\">";
        }
    }
}

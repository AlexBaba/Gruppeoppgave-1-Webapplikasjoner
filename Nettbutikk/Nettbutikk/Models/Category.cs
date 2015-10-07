using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nettbutikk.Models
{   
    public partial class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description
        {
            get;
            set;
        }
        
        public virtual Category ParentCategory { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }

        public new string ToString()
        {
            return "<" + GetType().FullName + " Name=\"" + Name + "\"," + " Description=\"" + Description + "\", " + " ParentCategory=\"" + ParentCategory.Name + "\">";
        }
    }
}

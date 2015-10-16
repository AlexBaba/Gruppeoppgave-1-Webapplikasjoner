    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace Nettbutikk.Models
{
    
    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, uint.MaxValue)]
        public uint Stock { get; set; }

        [InverseProperty("Products")]
        public virtual Category Category { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<Image> Images { get; set; }

        public string ImageUrl { get; set; }
    }
}

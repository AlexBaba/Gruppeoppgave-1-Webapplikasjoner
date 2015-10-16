using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    
    public partial class OrderLine
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Range(1, ulong.MaxValue)]
        public ulong Amount { get; set; }
        
        [Required]
        [InverseProperty("Items")]
        public virtual Order Order { get; set; }
        
        [Required]
        public virtual Product Product { get; set; }
    }
}

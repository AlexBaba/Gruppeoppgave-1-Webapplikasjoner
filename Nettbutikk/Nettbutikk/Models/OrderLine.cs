using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    
    public partial class OrderLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Range(1, ulong.MaxValue)]
        public ulong Amount { get; set; }
        
        [Required]
        public virtual Order Order { get; set; }
        
        [Required]
        public virtual Product Product { get; set; }
    }
}

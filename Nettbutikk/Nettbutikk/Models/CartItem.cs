using System;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikk.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public virtual Cart Cart { get; set; }
        
        [Required]
        public virtual Product Product { get; set; }
        
        public uint Amount { get; set; }
    }
}
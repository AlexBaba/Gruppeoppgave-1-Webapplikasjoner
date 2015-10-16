using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Nettbutikk.Models
{
    
    public partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }
        
        [Required]
        public DateTime PlacementDateTime { get; set; }

        [Required]
        [InverseProperty("Orders")]
        public virtual User Customer
        {
            get;
            set;
        }
        
        [Required]
        public virtual Address ShippingAddress
        {
            get;
            set;
        }

        [Required]
        public virtual Address BillingAddress
        {
            get;
            set;
        }
        
        [InverseProperty("Order")]
        public virtual ICollection<OrderLine> Items { get; set; }
    }
}

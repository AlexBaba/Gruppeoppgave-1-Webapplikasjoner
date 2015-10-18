using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Nettbutikk.Models
{
    public partial class User : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual Address PrimaryShippingAddress { get; set; }
        
        public virtual Address PrimaryBillingAddress { get; set; }

        public virtual CreditCard PaymentCard { get; set; }

        [InverseProperty("Addressee")]
        public virtual ICollection<Address> Addresses { get; set; }
        
        [InverseProperty("Customer")]
        public virtual ICollection<Order> Orders { get; set; }

        [InverseProperty("User")]
        public virtual Credential Cred { get; set; }

        public string FullName
        {
            get {
                return this.FirstName + " " + this.LastName;
            }
        }

        public class Credential
        {
            [Key]
            [Required]
            public string UserId
            {
                get;
                set;
            }

            [Required]
            public virtual User User
            {
                get;
                set;
            }

            [Required]
            public byte[] Password {
                get; set;
            }
        }
    }
}

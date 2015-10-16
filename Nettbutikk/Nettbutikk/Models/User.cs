﻿using System.Data.Entity;
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
    public class User : IdentityUser
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

        public string FullName
        {
            get {
                return this.FirstName + " " + this.LastName;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var identity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            identity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, this.FullName));

            return identity;
        }
    }
}

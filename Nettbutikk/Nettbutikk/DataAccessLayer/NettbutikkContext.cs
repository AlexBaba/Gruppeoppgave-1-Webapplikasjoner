
using Microsoft.AspNet.Identity.EntityFramework;
using Nettbutikk.Models;
using System.Data.Entity;

namespace Nettbutikk.DataAccessLayer
{
    public class NettbutikkContext : IdentityDbContext<User>
    {
        public NettbutikkContext()
            : base("Nettbutikk", throwIfV1Schema: false)
        {
        }

        public static NettbutikkContext Create()
        {
            return new NettbutikkContext();
        }
        
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }

    }
}
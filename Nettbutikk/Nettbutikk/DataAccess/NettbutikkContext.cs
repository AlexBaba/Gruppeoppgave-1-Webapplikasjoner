using Microsoft.AspNet.Identity.EntityFramework;
using Nettbutikk.Models;
using System.Data.Entity;

namespace Nettbutikk.DataAccess
{

    public class NettbutikkContext : IdentityDbContext<User>
    {
        public NettbutikkContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<Image> Images { get; set; }


        public static NettbutikkContext Create()
        {
            return new NettbutikkContext();
        }
    }
}
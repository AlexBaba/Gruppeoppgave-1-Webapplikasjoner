
using Microsoft.AspNet.Identity.EntityFramework;
using Nettbutikk.Models;

namespace Nettbutikk.DAL
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

        public System.Data.Entity.DbSet<Nettbutikk.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<Nettbutikk.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Nettbutikk.Models.Category> Categories { get; set; }
    }
}
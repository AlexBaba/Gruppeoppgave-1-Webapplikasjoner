namespace Nettbutikk.Migrations
{
    using System.Data.Entity.Migrations;
    using DataAccess;

    internal sealed class Configuration : DbMigrationsConfiguration<NettbutikkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NettbutikkContext context)
        {
            DbSeed.Seed(context);
        }
    }
}

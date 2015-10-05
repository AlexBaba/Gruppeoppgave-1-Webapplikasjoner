namespace Nettbutikk.Migrations
{
    using DAL;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<NettbutikkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Nettbutikk.DAL.NettbutikkContext";
        }

        protected override void Seed(NettbutikkContext db)
        {
            var adminRole = new IdentityRole("Administrator");
            var managerRole = new IdentityRole("Manager");

            var hasher = new PasswordHasher();
            
            db.Users.AddOrUpdate(u => u.UserName,
                new User
                {
                    UserName = "admin@example.com",
                    PasswordHash = hasher.HashPassword("password"),
                    Email = "admin@example.com"
                }
            );

            var tanks = new Category("Tanks", "Tanks");
            var cannons = new Category("Cannons", "Cannons");
            var engines = new Category("Engines", "Engines");
            // main categories.
            db.Categories.AddOrUpdate(c => c.Name,
                tanks,
                cannons,
                engines);

            db.SaveChanges();

            db.Categories.FindAsync(new { Name = "Tanks" }).ContinueWith(async category =>
            {
                db.Categories.AddOrUpdate(c => c.Name,
                    new Category(
                        "US Tanks",
                        "Tanks used by the American Forces",
                        await category
                    ),
                    new Category(
                        "USSR Tanks",
                        "Tanks used by the Russian Federation Forces",
                        await category
                    ),
                    new Category(
                        "UK Tanks",
                        "Tanks used by the British Forces",
                        await category
                    ),
                    new Category(
                        "German Tanks",
                        "Tanks used by the German Forces",
                        await category
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "Cannons" }).ContinueWith(async category =>
            {
                db.Categories.AddOrUpdate(c => c.Name,
                    new Category(
                        "US Cannons",
                        "Cannons used by the American Forces",
                        await category
                    ),
                    new Category(
                        "USSR Cannons",
                        "Cannons used by the Russian Federation Forces",
                        await category
                    ),
                    new Category(
                        "UK Cannons",
                        "Cannons used by the British Forces",
                        await category
                    ),
                    new Category(
                        "German Cannons",
                        "Cannons used by the German Forces",
                        await category
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "Engines" }).ContinueWith(async category =>
            {
                db.Categories.AddOrUpdate(c => c.Name,
                    new Category(
                        "US Engines",
                        "Engines used by the American Forces",
                        await category
                    ),
                    new Category(
                        "USSR Engines",
                        "Engines used by the Russian Federation Forces",
                        await category
                    ),
                    new Category(
                        "UK Engines",
                        "Engines used by the British Forces",
                        await category
                    ),
                    new Category(
                        "German Engines",
                        "Engines used by the German Forces",
                        await category
                    )
                );
            });

            Func<float> randomPriceGenerator = () =>
            {
                Random random = new Random();
                return (float)(((random.NextDouble() * 2.0) - 1.0) * (Math.Pow(2.0, random.Next(-126, 128))));
            };

            db.Categories.FindAsync(new { Name = "US Tanks" }).ContinueWith(async category =>
            {
                db.Products.AddOrUpdate(p => p.Name,
                    new Product(
                        "M4 Sherman",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/M4_Sherman",
                        await category
                    ),
                    new Product(
                        "M26 Pershing",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/M26_Pershing",
                        await category
                    ),
                    new Product(
                        "M10 Wolverine",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/M10_Wolverine",
                        await category
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "USSR Tanks" }).ContinueWith(async category =>
            {
                db.Products.AddOrUpdate(p => p.Name,
                    new Product(
                        "T-34",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/T-34",
                        await category
                    ),
                    new Product(
                        "KV-1",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/KV_tank",
                        await category
                    ),
                    new Product(
                        "SU-85",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/SU-85",
                        await category
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "UK Tanks" }).ContinueWith(async cateogry =>
            {
                db.Products.AddOrUpdate(p => p.Name,
                    new Product(
                        "Sherman Firefly",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Sherman_Firefly",
                        await cateogry
                    ),
                    new Product(
                        "Churchill Tank",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Churchill_tank",
                        await cateogry
                    ),
                    new Product("Light Tank Mk VI",
                    randomPriceGenerator(),
                    "https://en.wikipedia.org/wiki/Light_Tank_Mk_VI",
                    await cateogry
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "German Tanks" }).ContinueWith(async cateogry =>
            {
                db.Products.AddOrUpdate(
                    new Product(
                        "Panzerkampfwagen VI Tiger I",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Tiger_I",
                        await cateogry
                    ),
                    new Product(
                        "Panzerkampfwagen V Panther",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Panther_tank",
                        await cateogry
                    ),
                    new Product(
                        "JagdPanther",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Jagdpanther",
                        await cateogry
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "US Cannons" }).ContinueWith(async cateogry =>
            {
                db.Products.AddOrUpdate(p => p.Name,
                    new Product(
                        "75 mm Gun M2/M3/M6 (M4 Sherman)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/75_mm_Gun_M2/M3/M6#M3",
                        await cateogry
                    ),
                    new Product(
                        "90 mm Gun M1/M2/M3 (M26 Pershing)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/90_mm_Gun_M1/M2/M3",
                        await cateogry
                    ),
                    new Product(
                        "3-inch Gun M1918 (M10 Wolverine)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/3-inch_Gun_M1918",
                        await cateogry
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "USSR Cannons" }).ContinueWith(async cateogry =>
            {
                db.Products.AddOrUpdate(p => p.Name,
                    new Product(
                        "F-34 tank gun (T-34/KV)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/F-34_tank_gun",
                        await cateogry
                    ),
                    new Product(
                        "152 mm howitzer M1938 (M-10) (KV-1)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/152_mm_howitzer_M1938_(M-10)",
                        await cateogry
                    ),
                    new Product(
                        "85 mm air defense gun M1939 (52-K) (SU-85)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/D-5T",
                        await cateogry
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "UK Cannons" }).ContinueWith(async cateogry =>
            {
                db.Products.AddOrUpdate(p => p.Name,
                    new Product(
                        "OQF 17-pounder (Sherman Firefly)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Ordnance_QF_17-pounder",
                        await cateogry
                    ),
                    new Product(
                        "Ordnance QF 75 mm (Churchill Tank)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Ordnance_QF_75_mm",
                        await cateogry
                    ),
                    new Product(
                        ".50 in Vickers machine gun (Light Tank Mk VI)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/Vickers_.50_machine_gun",
                        await cateogry
                    )
                );
            });

            db.Categories.FindAsync(new { Name = "German Cannons" }).ContinueWith(async cateogry =>
            {
                db.Products.AddOrUpdate(p => p.Name,
                    new Product(
                        "8.8 cm KwK 36 (Tiger I)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/8.8_cm_KwK_36 ",
                        await cateogry
                    ),
                    new Product(
                        "7.5 cm KwK 42 (Panther)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/7.5_cm_KwK_42 ",
                        await cateogry
                    ),
                    new Product(
                        "3 - 8.8 cm Pak 43 (JagdPanther)",
                        randomPriceGenerator(),
                        "https://en.wikipedia.org/wiki/8.8_cm_Pak_43",
                        await cateogry
                    )
                );
            });
        }
    }
}

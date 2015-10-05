namespace Nettbutikk.Migrations
{
    using DAL;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NettbutikkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Nettbutikk.DAL.NettbutikkContext";
        }

        protected override void Seed(NettbutikkContext db)
        {
            if (!(db.Users.Any(u => u.UserName == "admin@example.com")))
            {
                var userStore = new UserStore<User>(db);
                var userManager = new UserManager<User>(userStore);
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var user = new User
                {
                    UserName = "admin@example.com",
                    PhoneNumber = "9876543210",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                userManager.Create(user, "password");

                db.SaveChanges();

                user = userManager.FindById(user.Id);
                
                if(!roleManager.RoleExists("Administrator"))
                {
                    roleManager.Create(new IdentityRole("Administrator"));

                    userManager.AddToRole(user.Id, "Administrator");
                }
            }

            var tanks = new Category { Name = "Tanks", Description = "Tanks" };
            var cannons = new Category { Name = "Cannons", Description = "Cannons" };
            var engines = new Category { Name = "Engines", Description = "Engines" };
            
            // main categories.
            db.Categories.Add(tanks);
            db.Categories.Add(cannons);
            db.Categories.Add(engines);

            db.SaveChanges();

            tanks = db.Categories.Find("Tanks");
            engines = db.Categories.Find("Engines");
            cannons = db.Categories.Find("Cannons");

            db.Categories.AddOrUpdate(c => c.Name,
                new Category
                {
                    Name = "US Tanks",
                    Description = "Tanks used by the American Forces",
                    ParentCategory = tanks
                },
                new Category
                {
                    Name = "USSR Tanks",
                    Description = "Tanks used by the Russian Federation Forces",
                    ParentCategory = tanks
                },
                new Category
                {
                    Name = "UK Tanks",
                    Description = "Tanks used by the British Forces",
                    ParentCategory = tanks
                },
                new Category
                {
                    Name = "German Tanks",
                    Description = "Tanks used by the German Forces",
                    ParentCategory = tanks
                }
            );

            db.Categories.AddOrUpdate(c => c.Name,
                new Category
                {
                    Name = "US Cannons",
                    Description = "Cannons used by the American Forces",
                    ParentCategory = cannons
                },
                new Category
                {
                    Name = "USSR Cannons",
                    Description = "Cannons used by the Russian Federation Forces",
                    ParentCategory = cannons
                },
                new Category
                {
                    Name = "UK Cannons",
                    Description = "Cannons used by the British Forces",
                    ParentCategory = cannons
                },
                new Category
                {
                    Name = "German Cannons",
                    Description = "Cannons used by the German Forces",
                    ParentCategory = db.Categories.Find("Cannons")
                }
            );

            db.Categories.AddOrUpdate(c => c.Name,
                new Category
                {
                    Name = "US Engines",
                    Description = "Engines used by the American Forces",
                    ParentCategory = engines
                },
                new Category
                {
                    Name = "USSR Engines",
                    Description = "Engines used by the Russian Federation Forces",
                    ParentCategory = engines
                },
                new Category
                {
                    Name = "UK Engines",
                    Description = "Engines used by the British Forces",
                    ParentCategory = engines
                },
                new Category
                {
                    Name = "German Engines",
                    Description = "Engines used by the German Forces",
                    ParentCategory = engines
                }
            );

            Func<float> randomPriceGenerator = () =>
            {
                Random random = new Random();
                return (float)(((random.NextDouble() * 2.0) - 1.0) * (Math.Pow(2.0, random.Next(-126, 128))));
            };
            var us_tanks = db.Categories.Find("US Tanks");
            db.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "M4 Sherman",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/M4_Sherman",
                    Category = us_tanks
                },
                new Product
                {
                    Name = "M26 Pershing",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/M26_Pershing",
                    Category = us_tanks
                },
                new Product
                {
                    Name = "M10 Wolverine",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/M10_Wolverine",
                    Category = us_tanks
                }
            );

            var ussr_tanks = db.Categories.Find("USSR Tanks");
            db.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "T-34",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/T-34",
                    Category = ussr_tanks
                },
                new Product
                {
                    Name = "KV-1",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/KV_tank",
                    Category = ussr_tanks
                },
                new Product
                {
                    Name = "SU-85",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/SU-85",
                    Category = ussr_tanks
                }
            );

            var uk_tanks = db.Categories.Find("UK Tanks");
            db.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "Sherman Firefly",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Sherman_Firefly",
                    Category = uk_tanks
                },
                new Product
                {
                    Name = "Churchill Tank",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Churchill_tank",
                    Category = uk_tanks
                },
                new Product
                {
                    Name = "Light Tank Mk VI",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Light_Tank_Mk_VI",
                    Category = uk_tanks
                }
            );

            var german_tanks = db.Categories.Find("German Tanks");
            db.Products.AddOrUpdate(
                new Product
                {
                    Name = "Panzerkampfwagen VI Tiger I",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Tiger_I",
                    Category = german_tanks
                },
                new Product
                {
                    Name = "Panzerkampfwagen V Panther",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Panther_tank",
                    Category = german_tanks
                },
                new Product
                {
                    Name = "JagdPanther",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Jagdpanther",
                    Category = german_tanks
                }
            );

            var us_cannons = db.Categories.Find("US Cannons");
            db.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "75 mm Gun M2/M3/M6 (M4 Sherman)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/75_mm_Gun_M2/M3/M6#M3",
                    Category = us_cannons
                },
                new Product
                {
                    Name = "90 mm Gun M1/M2/M3 (M26 Pershing)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/90_mm_Gun_M1/M2/M3",
                    Category = us_cannons
                },
                new Product
                {
                    Name = "3-inch Gun M1918 (M10 Wolverine)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/3-inch_Gun_M1918",
                    Category = us_cannons
                }
            );

            var ussr_cannons = db.Categories.Find("USSR Cannons");
            db.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "F-34 tank gun (T-34/KV)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/F-34_tank_gun",
                    Category = ussr_cannons
                },
                new Product
                {
                    Name = "152 mm howitzer M1938 (M-10) (KV-1)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/152_mm_howitzer_M1938_(M-10)",
                    Category = ussr_cannons
                },
                new Product
                {
                    Name = "85 mm air defense gun M1939 (52-K) (SU-85)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/D-5T",
                    Category = ussr_cannons
                }
            );

            var uk_cannons = db.Categories.Find("UK Cannons");
            db.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "OQF 17-pounder (Sherman Firefly)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Ordnance_QF_17-pounder",
                    Category = uk_cannons
                },
                new Product
                {
                    Name = "Ordnance QF 75 mm (Churchill Tank)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Ordnance_QF_75_mm",
                    Category = uk_cannons
                },
                new Product
                {
                    Name = ".50 in Vickers machine gun (Light Tank Mk VI)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/Vickers_.50_machine_gun",
                    Category = uk_cannons
                }
            );

            var german_cannons = db.Categories.Find("German Cannons");
            db.Products.AddOrUpdate(p => p.Name,
                new Product
                {
                    Name = "8.8 cm KwK 36 (Tiger I)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/8.8_cm_KwK_36 ",
                    Category = german_cannons
                },
                new Product
                {
                    Name = "7.5 cm KwK 42 (Panther)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/7.5_cm_KwK_42 ",
                    Category = german_cannons
                },
                new Product
                {
                    Name = "3 - 8.8 cm Pak 43 (JagdPanther)",
                    Price = randomPriceGenerator(),
                    Description = "https://en.wikipedia.org/wiki/8.8_cm_Pak_43",
                    Category = german_cannons
                }
            );
        }
    }
}

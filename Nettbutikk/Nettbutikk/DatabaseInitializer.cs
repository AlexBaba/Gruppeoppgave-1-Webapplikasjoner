using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Final_Nettbutikk.DAL;


namespace Nettbutikk_Base_Test
{
    public class DatabaseInitializer
    {

        private static bool stopIfExceptionIsCaught = false;

        //Seed all tables
        public static void seedAll()
        {

            NettbutikkEntities db = new NettbutikkEntities();

            try
            {
                seedPoststed(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding poststed: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            try
            {
                seedBilde(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding bilde: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            try
            {
                seedKategori(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding kategori: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            try
            {
                seedPerson(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding person: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            try
            {
                seedKunde(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding kunde: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            try
            {
                seedPassord(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding passord: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            try
            {
                seedOrdre(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding ordre: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            try
            {
                seedProdukt(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding produkt: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }


            try
            {
                seedBestilling(db);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding bestilling: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            System.Diagnostics.Debug.WriteLine("Seeded the database successfully");

        }


        public static void seedPoststed(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding poststed");

            var poststeder = new List<Poststed> {
                new Poststed() { Postnummer = 0, Sted = "bærum"},
                new Poststed() { Postnummer = 1, Sted = "oslo"},
                new Poststed() { Postnummer = 2, Sted = "trondheim" }
            };


            poststeder.ForEach(p => db.Poststed.Add(p));
            db.SaveChanges();

        }


        public static void seedPerson(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding person");

            var personer = new List<Person> {
                new Person { PersonId = 0, Fornavn = "Alexander", Etternavn = "Gård",
                Adresse = "Capralhaugen 59", Email = "alex.gaard@hotmail.com",
                Telefon = "90788086", Postnummer = 0},

                new Person { PersonId = 1, Fornavn = "Bob", Etternavn = "Gård",
                Adresse = "Storgata 21", Email = "bob@bob.bob",
                Telefon = "1881", Postnummer = 1},

                new Person { PersonId = 2, Fornavn = "Gunnar", Etternavn = "Gård",
                Adresse = "Gunnarveien 9", Email = "gunnar@gun.gun",
                Telefon = "786873242", Postnummer = 2}
            };

            personer.ForEach(p => db.Person.Add(p));
            db.SaveChanges();
        }


        public static void seedKunde(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding kunde");
            var kunder = new List<Kunde> {
                new Kunde() { KundeId = 0, PersonId = 0},
                new Kunde() { KundeId = 1, PersonId = 1},
                new Kunde() { KundeId = 2, PersonId = 2}
            };

            kunder.ForEach(k => db.Kunde.Add(k));
            db.SaveChanges();
        }


        public static void seedKategori(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding kategori");

            var kategorier = new List<Kategori> {
                new Kategori() { KategoriId = 0, Navn = "Tanks"},
                new Kategori() { KategoriId = 1, Navn = "Motorer"},
                new Kategori() { KategoriId = 2, Navn = "Kanoner"}
            };

            kategorier.ForEach(k => db.Kategori.Add(k));
            db.SaveChanges();
        }


        public static void seedBestilling(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding bestilling");
        }


        public static void seedBilde(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding bilde");


            var bilder = new List<Bilde> {
                new Bilde() { BildeId = 0, ProduktId = 0, BildeSti = "/Images/tanks/ChurchillTank.png" },

                new Bilde() { BildeId = 1, ProduktId = 1, BildeSti = "/Images/tanks/Jagdpanther.png" },

                new Bilde() { BildeId = 2, ProduktId = 2, BildeSti = "/Images/tanks/LightTankMkVI.png" },

                new Bilde() { BildeId = 3, ProduktId = 3, BildeSti = "/Images/tanks/M4Sherman.png" },

                new Bilde() { BildeId = 4, ProduktId = 4, BildeSti = "/Images/tanks/M10tankdestroyer.png" },

                new Bilde() { BildeId = 5, ProduktId = 5, BildeSti = "/Images/tanks/M26Pershing.png" },

                new Bilde() { BildeId = 6, ProduktId = 6, BildeSti = "/Images/tanks/Tiger1.png" },

                new Bilde() { BildeId = 7, ProduktId = 7, BildeSti = "/Images/tanks/ShermanFirefly.png" },

                new Bilde() { BildeId = 8, ProduktId = 8, BildeSti = "/Images/tanks/Panther.png" },

                new Bilde() { BildeId = 9, ProduktId = 9, BildeSti = "/Images/tanks/T54.png" },

                new Bilde() { BildeId = 10, ProduktId = 10, BildeSti = "/Images/tanks/Centuriontank.png" },

                new Bilde() { BildeId = 11, ProduktId = 11, BildeSti = "/Images/tanks/Zulfiqar.png" },

                new Bilde() { BildeId = 12, ProduktId = 12, BildeSti = "/Images/tanks/Stridsvagn122.png" },

                new Bilde() { BildeId = 13, ProduktId = 13, BildeSti = "/Images/tanks/T72m4cz.png" },

                new Bilde() { BildeId = 14, ProduktId = 14, BildeSti = "/Images/tanks/T-90SM.png" },

                new Bilde() { BildeId = 15, ProduktId = 15, BildeSti = "/Images/tanks/tanks1.jpg" },

                new Bilde() { BildeId = 16, ProduktId = 16, BildeSti = "/Images/tanks/tanks1.jpg" },

                new Bilde() { BildeId = 17, ProduktId = 17, BildeSti = "/Images/tanks/tanks1.jpg" },

                new Bilde() { BildeId = 18, ProduktId = 18, BildeSti = "/Images/kanoner/ChryslerA57multibank.jpg" },

                new Bilde() { BildeId = 19, ProduktId = 19, BildeSti = "/Images/kanoner/FordGAAengine.jpg" },

                new Bilde() { BildeId = 20, ProduktId = 20, BildeSti = "/Images/kanoner/ KharkivmodelV2.jpg" },

                new Bilde() { BildeId = 21, ProduktId = 21, BildeSti = "/Images/kanoner/Kharkivmodel2K.jpg" },

                new Bilde() { BildeId = 22, ProduktId = 22, BildeSti = "/Images/kanoner/ KharkivmodelV2.jpg" },

                new Bilde() { BildeId = 23, ProduktId = 23, BildeSti = "/Images/kanoner/GM6046dieselconjoined671.jpg" },

                new Bilde() { BildeId = 24, ProduktId = 24, BildeSti = "/Images/kanoner/ KharkivmodelV2.jpg" },

                new Bilde() { BildeId = 25, ProduktId = 25, BildeSti = "/Images/kanoner/Meadows6cylinder.jpg" },

                new Bilde() { BildeId = 26, ProduktId = 26, BildeSti = "/Images/kanoner/MaybachHL230P45.jpg" },

                new Bilde() { BildeId = 27, ProduktId = 27, BildeSti = "/Images/kanoner/MaybachHL230P30.jpg" },

                new Bilde() { BildeId = 28, ProduktId = 28, BildeSti = "/Images/kanoner/MaybachHL210P45.jpg" },

                new Bilde() { BildeId = 29, ProduktId = 29, BildeSti = "/Images/kanoner/75mmGunM2.jpg" },

                new Bilde() { BildeId = 30, ProduktId = 30, BildeSti = "/Images/kanoner/90mmgunM1.jpg" },

                new Bilde() { BildeId = 31, ProduktId = 31, BildeSti = "/Images/kanoner/3inchgunM1918.jpg" },

                new Bilde() { BildeId = 32, ProduktId = 32, BildeSti = "/Images/kanoner/76mmGunM2.jpg" },

                new Bilde() { BildeId = 33, ProduktId = 33, BildeSti = "/Images/kanoner/152mmhowitzerM1938.jpg" },

                new Bilde() { BildeId = 34, ProduktId = 34, BildeSti = "/Images/kanoner/85mmairdefensegunM193952K.jpg" },

                new Bilde() { BildeId = 35, ProduktId = 35, BildeSti = "/Images/kanoner/ OrdnanceQF75mm.jpg" },

                new Bilde() { BildeId = 36, ProduktId = 36, BildeSti = "/Images/kanoner/50calinVickersmachinegun.jpg" },

                new Bilde() { BildeId = 37, ProduktId = 37, BildeSti = "/Images/kanoner/88cmKwK36.jpg" },

                new Bilde() { BildeId = 38, ProduktId = 38, BildeSti = "/Images/kanoner/75cmKwK42.jpg" },

                new Bilde() { BildeId = 39, ProduktId = 39, BildeSti = "/Images/kanoner/88cmPak43.jpg" },

            };

            bilder.ForEach(b => db.Bilde.Add(b));
            db.SaveChanges();

        }


        public static void seedOrdre(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding ordre");
        }


        public static void seedPassord(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding passord");

            string salt = "";

            var passord = new List<Passord>
            {
                new Passord() { PersonId = 0, Salt = (salt = PasswordHandler.generateSalt()),
                Hash = PasswordHandler.createSaltedHash(salt, "password") },

                new Passord() { PersonId = 1, Salt = (salt = PasswordHandler.generateSalt()),
                Hash = PasswordHandler.createSaltedHash(salt, "password123") },

                new Passord() { PersonId = 2, Salt = (salt = PasswordHandler.generateSalt()),
                Hash = PasswordHandler.createSaltedHash(salt, "password123") },

            };

            passord.ForEach(p => db.Passord.Add(p));
            db.SaveChanges();
        }

        public static void seedProdukt(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding produkt");

            var produkter = new List<Produkt> {
                 new Produkt() { ProduktId = 0, KategoriId = 0,
                 Navn = "Churchill tank", Pris = 3050,
                 Beskrivelse = "The Tank, Infantry, Mk IV (A22) was a British heavy infantry tank used in the Second World War, best known for its heavy armour, large longitudinal chassis with all-around tracks with multiple bogies, and its use as the basis of many specialist vehicles. It was one of the heaviest Allied tanks of the war."},


                 new Produkt() { ProduktId = 1, KategoriId = 0,
                 Navn = "Jagdpanther", Pris = 4400,
                 Beskrivelse = "The Jagdpanther (German: \"hunting panther\") was a tank destroyer built by Nazi Germany during World War II based on the chassis of the Panther tank. It entered service late in the war (1944) and saw service on the Eastern and Western Fronts. The Jagdpanther combined the very powerful 8.8 cm PaK 43 cannon of the Tiger II and the characteristically excellent armor and suspension of the Panther chassis,[1] although it suffered from the general poor state of German ordnance production, maintenance and training in the later part of the war, which resulted in small production numbers, shortage in spare parts and poor crew readiness." },


                 new Produkt() { ProduktId = 2, KategoriId = 0,
                 Navn = "Light Tank Mk VI", Pris = 1350,
                 Beskrivelse = "The Tank, Light, Mk VI was a British light tank, produced by Vickers-Armstrongs in the late 1930s, which saw service during World War II." },

                 new Produkt() { ProduktId = 3, KategoriId = 0,
                 Navn = "M4 Sherman", Pris = 23350,
                 Beskrivelse = "The M4 Sherman, officially Medium Tank, M4, was the most numerous battle tank used by the United States and some other Western Allies in World War II. It proved to be reliable and mobile. In spite of being outclassed by German medium and heavy tanks late in the war, the M4 Sherman was cheaper to produce and available in greater numbers." },

                 new Produkt() { ProduktId = 4, KategoriId = 0,
                 Navn = "M10 tank destroyer", Pris = 36550,
                 Beskrivelse = "The M10 tank destroyer was a United States tank destroyer of World War II based on the chassis of the M4 Sherman tank fitted with the 3-inch (76.2 mm) Gun M7. Formally 3-inch Gun Motor Carriage, M10, it was numerically the most important U.S. tank destroyer of World War II and combined a reasonably potent anti-tank weapon with a turreted platform (unlike the previous M3 GMC, whose gun was capable of only limited traverse)." },

                 new Produkt() { ProduktId = 5, KategoriId = 0,
                 Navn = "M26 Pershing", Pris = 7850,
                 Beskrivelse = "The Pershing was a heavy tank of the United States Army. It was designated a heavy tank when it was first designed in World War II due to its 90mm gun, and its armor. The tank is named after General of the Armies John J. Pershing, who led the American Expeditionary Force in Europe in World War I. It was briefly used both in World War II and the Korean War." },

                 new Produkt() { ProduktId = 6, KategoriId = 0,
                 Navn = "Tiger 1", Pris = 8600,
                 Beskrivelse = "Tiger I is the common name of a German heavy tank developed in 1942 and used in World War II. The final official German designation was Panzerkampfwagen VI Tiger Ausf. E, often shortened to Tiger." },

                 new Produkt() { ProduktId = 7, KategoriId = 0,
                 Navn = "Sherman Firefly", Pris = 3200,
                 Beskrivelse = "The Sherman Firefly was a tank used by the United Kingdom in World War II. It was based on the US M4 Sherman but fitted with the powerful 3-inch (76.2 mm) calibre British 17-pounder anti-tank gun as its main weapon. Originally conceived as a stopgap until future British tank designs came into service, the Sherman Firefly became the most common vehicle with the 17-pounder in the war." },

                 new Produkt() { ProduktId = 8, KategoriId = 0,
                 Navn = "Panther tank", Pris = 99955,
                 Beskrivelse = "The Panther was a German medium tank deployed during World War II from mid-1943 to the end of the European war in 1945." },

                 new Produkt() { ProduktId = 9, KategoriId = 0,
                 Navn = "T-54", Pris = 89930,
                 Beskrivelse = "T-54 tanks are a serie of main battle tank introduced just as the Second World War ended. The first T-54 prototype appeared in March 1945 and entered full production in 1947. It became the main tank for armoured units of the Soviet Army, armies of the Warsaw Pact countries, and many others. " },

                 new Produkt() { ProduktId = 10, KategoriId = 0,
                 Navn = "Centurion", Pris = 70000,
                 Beskrivelse = "The Centurion, introduced in 1945, was the primary British main battle tank of the post-World War II period. It is widely considered to be one of the most successful post-war tank designs, remaining in production into the 1960s, and seeing combat in the front lines into the 1980s." },

                 new Produkt() { ProduktId = 11, KategoriId = 0,
                 Navn = "Zulfiqar", Pris = 75000,
                 Beskrivelse = "Zulfiqar is an Iranian main battle tank (MBT), conceived by Brigadier General Mir-Younes Masoumzadeh, deputy ground force commander for research and self-sufficiency of the armed forces." },

                 new Produkt() { ProduktId = 12, KategoriId = 0,
                 Navn = "Stridsvagn 122", Pris = 9999,
                 Beskrivelse = "Stridsvagn 122  (\"Battle Tank 122\") is a Swedish main battle tank based on the German Leopard 2. As with the Leopard 2A5 it is based on the German Leopard 2 Improved variant, utilizing newer technology such as command, control, and fire control systems, as well as reinforced armour and long-term combat capacity." },

                 new Produkt() { ProduktId = 13, KategoriId = 0,
                 Navn = "T-72m4cz", Pris = 12000,
                 Beskrivelse = "The T-72M4CZ is an upgraded Czech version of the Soviet-made main battle tank T-72. The only user of this tank is the Czech Army." },

                 new Produkt() { ProduktId = 14, KategoriId = 0,
                 Navn = "Type-90SM", Pris = 20400,
                 Beskrivelse = "The T-90 is a Russian third-generation main battle tank that is essentially a modernisation of the T-72B, incorporating many features of the T-80U (it was originally to be called the T-72BU, later renamed to T-90). It is currently the most modern tank in service with the Russian Ground Forces and Naval Infantry. " },

                 new Produkt() { ProduktId = 15, KategoriId = 0,
                 Navn = "T-14 Armata", Pris = 50060,
                 Beskrivelse = "The T-14 Armata is a Russian 5th generation main battle tank based on the Armata Universal Combat Platform." },

                 new Produkt() { ProduktId = 16, KategoriId = 0,
                 Navn = "K2 Black Panther", Pris = 90805,
                 Beskrivelse = "The K2 Black Panther is a South Korean main battle tank that will replace most of theM48 Patton tanks and complement the K1 series of main battle tanks currently fielded by the Republic of Korea." },

                 new Produkt() { ProduktId = 17, KategoriId = 0,
                 Navn = "Type 10", Pris = 40050,
                 Beskrivelse = "The Type 10 is a 4th generation main battle tank that the Japan Ground Self Defense Force has been equipped with, and boasts significant enhancements in its capability to respond to anti-tank warfare, mobile strikes, special operations force attacks, and other contingencies. " },



                 new Produkt() { ProduktId = 18, KategoriId = 1,
                 Navn = "Chrysler A57 multibank", Pris = 1000,
                 Beskrivelse = "Created in 1941 as America entered World War II, the A57 Multibank engine was born out of the necessity for a rear-mounted tank engine to be developed and produced, in the shortest time possible, for use in both the 109 examples built of the M3A4 Medium Tank, and the 7,499 examples built of the successor M4A4 Medium tank, each of which had lengthened hulls to accommodate them."},

                 new Produkt() { ProduktId = 19, KategoriId = 1,
                 Navn = "Ford GAA engine", Pris = 1000,
                 Beskrivelse = "The Ford GAA engine is an American all-aluminum 32-valve DOHC 60-degree V8 engine engineered and produced by the Ford Motor Company just before, and during, World War II. It featured twin Stromberg NA-Y5-G carburetors, dual magnetos and twin spark plugs making up a full dual ignition system, and crossflow induction. It displaces 1,100 cu in (18 l) and puts out well over 1,000 pound-feet (1,400 N·m) of torque from idle to 2600 rpm. The factory-rated output was 525 hp (391 kW) @ 2800 rpm. In terms of its capacity, the GAA was the largest mass-produced gasoline V8 engine in the world."},

                 new Produkt() { ProduktId = 20, KategoriId = 1,
                 Navn = "Kharkiv model V-2-34", Pris = 1000,
                 Beskrivelse = "The Kharkiv model V-2 was a Soviet diesel tank V-12 engine designed at the Kharkiv Locomotive Factory by Konstantin Chelpan and his team. It is found in the BT-7M (BT-8), T-34, KV, IS and IS-10 (T-10) tanks, and by extension, the vehicles based on them, such as the SU-85 and SU-100 tank destroyers based on the T-34 and the ISU-122 and ISU-152 self-propelled guns based on the IS-2. Throughout its production life, output ranged from roughly 450-700 hp.V-2 with revised hull mounts, fuel and cooling connectors and refined clutch, 1939. Used in the T-34, SU-85 and SU-100. It produced 500 hp @ 1800 RPM."},

                 new Produkt() { ProduktId = 21, KategoriId = 1,
                 Navn = "Kharkiv model V-2K", Pris = 1000,
                 Beskrivelse = "The Kharkiv model V-2 was a Soviet diesel tank V-12 engine designed at the Kharkiv Locomotive Factory by Konstantin Chelpan and his team. It is found in the BT-7M (BT-8), T-34, KV, IS and IS-10 (T-10) tanks, and by extension, the vehicles based on them, such as the SU-85 and SU-100 tank destroyers based on the T-34 and the ISU-122 and ISU-152 self-propelled guns based on the IS-2. Throughout its production life, output ranged from roughly 450-700 hp.V-2 with increased injection pressure and higher engine speed, 1939. Used in the KV-1 and KV-2. It produced 600 hp."},

                 new Produkt() { ProduktId = 22, KategoriId = 1,
                 Navn = "Kharkiv model V-2", Pris = 1000,
                 Beskrivelse = "The Kharkiv model V-2 was a Soviet diesel tank V-12 engine designed at the Kharkiv Locomotive Factory by Konstantin Chelpan and his team. It is found in the BT-7M (BT-8), T-34, KV, IS and IS-10 (T-10) tanks, and by extension, the vehicles based on them, such as the SU-85 and SU-100 tank destroyers based on the T-34 and the ISU-122 and ISU-152 self-propelled guns based on the IS-2. Throughout its production life, output ranged from roughly 450-700 hp.Initial production version, 1937. Used in the BT-7M (BT-8)."},

                 new Produkt() { ProduktId = 23, KategoriId = 1,
                 Navn = "GM 6046 diesel (conjoined 6-71s)", Pris = 1000,
                 Beskrivelse = "The M4A2 version of the Sherman tank was powered by the General Motors 6046D twin diesel engine, a 12-cylinder twin bank version of the General Motors series 71 six cylinder supercharged two-stroke diesel. Each six cylinder engine unit displaced 6,965cc, and was separately clutched to a single output shaft, which was itself clutched to the transmission unit. The whole engine weighed 2,323 kg (5,110 lbs) dry weight, and produced up to 410 horsepower at 2,900 rpm with both units running. A total of 10,968 6046D-powered M4A2 Shermans were produced. "},

                 new Produkt() { ProduktId = 24, KategoriId = 1,
                 Navn = "Flat-twelve engine", Pris = 1000,
                 Beskrivelse = "A flat-12 is a 12-cylinder internal combustion engine in a flat configuration. Rarer, wider, and less tall than a V12, a flat-12 is used exclusively in mid-engined sports cars and rarely on production cars. The engine has a naturally lower center of gravity than a V12, but must be mounted somewhat higher in the engine bay to provide clearance for the exhaust system (six runners on either side)."},

                 new Produkt() { ProduktId = 25, KategoriId = 1,
                 Navn = "Meadows 6-cylinder", Pris = 1000,
                 Beskrivelse = "In the 1930s Meadows developed a flat-12 type-MAT/1 engine of 8858cc for military applications including the Tetrarch Light Tank. Later they built a 16litre 300 bhp flat-12 type-DAV petrol engine used in the Covenanter tank. This engine was also used in the prototype A20 tank, although this weighed more than twice the Covenanter and so was considered underpowered. The later, and widely used, A22 Churchill tank was a development of the A20. Partly to provide more power, and also to improve production time, this was instead powered by a Vauxhall flat-12 engine termed the \"Twin-Six\", as it was based on two pre-existing Bedford six-cylinder lorry engines. This engine was slightly more powerful, but only to a rated 350 bhp. The Guy Armoured Car, made in 1939–1940, used the Meadows 4-cylinder 4-ELA petrol engine."},

                 new Produkt() { ProduktId = 26, KategoriId = 1,
                 Navn = "Maybach HL230 P45", Pris = 1000,
                 Beskrivelse = "The Maybach HL230 P45 was a water-cooled 60° 23 liter V12 gasoline engine designed by Maybach. It was used during World War II in later versions of the Tiger I."},

                 new Produkt() { ProduktId = 27, KategoriId = 1,
                 Navn = "Maybach HL230 P30", Pris = 1000,
                 Beskrivelse = "The Maybach HL230 P30 was a water-cooled 60° 23 liter V12 gasoline engine designed by Maybach. It was used during World War II in the Panther."},

                 new Produkt() { ProduktId = 28, KategoriId = 1,
                 Navn = "Maybach HL 210 P45", Pris = 1000,
                 Beskrivelse = "The Maybach HL210 P45 was a water-cooled 60° 23 liter V12 gasoline engine designed by Maybach. It was used during World War II in the Jagdpanther.HL210 had a displacement of 21 liters and an aluminium crankcase and block."},

                 new Produkt() { ProduktId = 29, KategoriId = 2,
                 Navn = "75 mm gun M2/M3", Pris = 1000,
                 Beskrivelse = "The US 75 mm gun tank gun M2 and the later M3 were the standard American tank guns of the Second World War, used primarily on the two main American medium tanks of the war, the M3 Lee (M2 or M3 gun) and the M4 Sherman (M3 gun)."},

                 new Produkt() { ProduktId = 30, KategoriId = 2,
                 Navn = "90 mm Gun M1/M2/M3 ", Pris = 1000,
                 Beskrivelse = "The 90 mm Gun M1/M2/M3 served as a primary heavy American anti-aircraft and anti-tank gun, playing a role similar to the renowned German 88 mm gun. It was 90 mm (3.5 in) in caliber, and had a 4.60 m (15 ft) barrel, 53 calibers in length. It was capable of firing a 90×600 mm R shell 17,823 m (58,474 ft) horizontally, or a maximum altitude of 10,380 m (34,060 ft)."},

                 new Produkt() { ProduktId = 31, KategoriId = 2,
                 Navn = "3-inch M1918 gun", Pris = 1000,
                 Beskrivelse = "The 3-inch M1918 gun was a United States 3-inch anti-aircraft gun that entered service in 1918 and served until it was finally superseded by the 3\" M3 gun in 1930. The M3 was subsequently replaced by the M1 90mm AA gun just prior to the opening of World War II. The M3 3\" gun was later adapted for the anti-tank role, serving as the main armament of the M10 tank destroyer during World War II."},

                 new Produkt() { ProduktId = 32, KategoriId = 2,
                 Navn = "76 mm tank gun M1940 F-34 ", Pris = 1000,
                 Beskrivelse = "The 76 mm tank gun M1940 F-34 was a 76.2 mm Soviet tank gun used on the T-34/76 tank. A modified version of the gun, the 76 mm tank gun M1941 ZiS-5, was used on KV-1 tanks during World War II. Nowadays, the two versions are often referred to just by their factory designations, as \"F-34\" and \"ZiS-5\", respectively."},

                 new Produkt() { ProduktId = 33, KategoriId = 2,
                 Navn = "152-mm howitzer M1938 (M-10)", Pris = 1000,
                 Beskrivelse = "152-mm howitzer M1938 (M-10) was a Soviet 152.4 mm (6 inch) howitzer of World War II era. It was developed in 1937–1938 at the Motovilikha Mechanical Plant by a team headed by F. F. Petrov. Although production of the gun was stopped in 1941, it saw combat with the Red Army until the end of World War II and remained in service until the 1950s. Captured pieces were used by Wehrmacht and the Finnish Army. The latter kept the M-10 in service until 2000. In a tank-mounted variant, M-10T, the gun was mounted on the KV-2 heavy tank."},

                 new Produkt() { ProduktId = 34, KategoriId = 2,
                 Navn = "85 mm air defense gun M1939 (52-K)", Pris = 1000,
                 Beskrivelse = "The 85 mm air defense gun M1939 (52-K) was an 85-mm Soviet air defense gun, developed under guidance of leading Soviet designers M. N. Loginov and G. D. Dorokhin. This gun was successfully used throughout the German-Soviet War against level bombers and other high- and medium-altitude targets. In emergencies they were utilized as powerful anti-tank weapons. The barrel of the 52-K was the basis for the family of 85-mm Soviet tank guns. Crews of 85-mm AD guns shot down 4,047 Axis aircraft. The mean quantity of 85-mm ammunition required to shoot down one enemy plane was 598 rounds. After the war some 52-Ks were refitted for peaceful purposes as anti-avalanche guns in mountainous terrain."},

                 new Produkt() { ProduktId = 35, KategoriId = 2,
                 Navn = "Ordnance QF 75 mm", Pris = 1000,
                 Beskrivelse = "The Ordnance QF 75 mm, abbreviated to OQF 75 mm, was a British tank-gun of the Second World War. It was obtained by boring out the Ordnance QF 6 pounder (\"6 pdr\") 57-mm anti-tank gun to 75-mm, to give better performance against infantry targets in a similar fashion to the 75 mm gun fitted to the American Sherman tank."},

                 new Produkt() { ProduktId = 36, KategoriId = 2,
                 Navn = "Vickers .50 machine gun", Pris = 1000,
                 Beskrivelse = "The Vickers .50 machine gun, also known as the 'Vickers .50' was basically similar to the .303 inches (7.70 mm) Vickers machine gun but scaled up to use a larger-calibre 0.5-inch (12.7 mm) round. It saw some use in tanks and other fighting vehicles, but was much more commonly used as close-in anti-aircraft weapons on Royal Navy and allied ships, typically in a four-gun mounting. The Vickers fired UK 12.7×81mm 50-calibre ammunition, not the better known US 12.7×99mm (.50 BMG)."},

                 new Produkt() { ProduktId = 37, KategoriId = 2,
                 Navn = "8.8 cm KwK 36 L/56 ", Pris = 1000,
                 Beskrivelse = "The 8.8 cm KwK 36 L/56 was an 88 mm electrically fired tank gun used by the German Heer during World War II. This was the primary weapon of the PzKpfw VI Tiger I tank. It was developed and built by Krupp."},

                 new Produkt() { ProduktId = 38, KategoriId = 2,
                 Navn = "7.5 cm KwK 42 L/70 ", Pris = 1000,
                 Beskrivelse = "The 7.5 cm KwK 42 L/70 was a 7.5 cm calibre German tank gun developed and built by Rheinmetall-Borsig AG in Unterlüß during the Second World War. The gun was used to equip the SdKfz.171 Panther medium tank and the SdKfz.162/1 Jagdpanzer IV/70(A)/(V) tank destroyer. When mounted on a tank destroyer it was designated as the 7.5 cm Pak 42."},

                 new Produkt() { ProduktId = 39, KategoriId = 2,
                 Navn = "8.8 cm PaK 43", Pris = 1000,
                 Beskrivelse = "The Pak 43 was a German 88 mm anti-tank gun developed by Krupp in competition with the Rheinmetall 8.8 cm Flak 41 anti-aircraft gun and used during the Second World War. The Pak 43 was the most powerful anti-tank gun of the Wehrmacht to see service in significant numbers, also serving in modified form as the 8.8 cm KwK 43 main gun on the Tiger II tank, and Elefant, Jagdpanther and Nashorn tank destroyers."},


            };

            produkter.ForEach(p => db.Produkt.Add(p));
            db.SaveChanges();

        }


    }
}
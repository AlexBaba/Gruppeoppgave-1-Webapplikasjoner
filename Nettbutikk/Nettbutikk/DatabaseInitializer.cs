using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Local_Nettbutikk.DAL;

namespace Nettbutikk_Base_Test
{
    public class DatabaseInitializer
    {

        private static bool stopIfExceptionIsCaught = false;

        //Seed all tables
        public static void seedAll() {

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
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("Caught exception while seeding bestilling: " + ex.ToString());
                if (stopIfExceptionIsCaught)
                    return;
            }

            System.Diagnostics.Debug.WriteLine("Seeded the database successfully");

        }


        public static void seedPoststed(NettbutikkEntities db) {
            System.Diagnostics.Debug.WriteLine("Seeding poststed");

            var poststeder = new List<Poststed> {
                new Poststed() { Postnummer = 0, Sted = "bærum"},
                new Poststed() { Postnummer = 1, Sted = "oslo"},
                new Poststed() { Postnummer = 2, Sted = "trondheim" }
            };

            
            poststeder.ForEach(p => db.Poststed.Add(p));
            db.SaveChanges();

        }


        public static void seedPerson(NettbutikkEntities db) {
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
                new Kategori() { KategoriId = 0, Navn = "Hageutstyr"},
                new Kategori() { KategoriId = 1, Navn = "Båt"},
                new Kategori() { KategoriId = 2, Navn = "Bil"},
                new Kategori() { KategoriId = 3, Navn = "Tanks"}
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
                new Bilde() { BildeId = 0, ProduktId = 0, BildeSti = "/Images/spade.jpg" },
                new Bilde() { BildeId = 1, ProduktId = 1, BildeSti = "/Images/gressklipper.jpg" },
                new Bilde() { BildeId = 2, ProduktId = 2, BildeSti = "/Images/fotball.jpg" },
                new Bilde() { BildeId = 3, ProduktId = 3, BildeSti = "/Images/murstein.jpg" }

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

            string salty = "";

            /*
            new Passord() { PersonId = 0, Salt = (salty = PasswordHandler.generateSalt()),
                Passord1 = PasswordHandler.createSaltedHash(salty, "password") },
            */

            var passord = new List<Passord>
            {
                new Passord() { PersonId = 0, Salt = (salty = PasswordHandler.generateSalt()),
                Hash = PasswordHandler.createSaltedHash(salty, "password") },

                new Passord() { PersonId = 1, Salt = (salty = PasswordHandler.generateSalt()),
                Hash = PasswordHandler.createSaltedHash(salty, "password123") },

                new Passord() { PersonId = 2, Salt = (salty = PasswordHandler.generateSalt()),
                Hash = PasswordHandler.createSaltedHash(salty, "password123") },

            };

            passord.ForEach(p => db.Passord.Add(p));
            db.SaveChanges();
        }

        public static void seedProdukt(NettbutikkEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Seeding produkt");
            var produkter = new List<Produkt> {
                new Produkt() { ProduktId = 0, KategoriId = 0,
                Navn = "Spade", Pris = 350, Beskrivelse = ""},
                new Produkt() { ProduktId = 1, KategoriId = 0,
                Navn = "Gressklipper", Pris = 2499, Beskrivelse = ""},
                new Produkt() { ProduktId = 2, KategoriId = 0,
                Navn = "Fotball", Pris = 99, Beskrivelse = ""},
                new Produkt() { ProduktId = 3, KategoriId = 0,
                Navn = "Murstein", Pris = 50, Beskrivelse = ""}

            };

            produkter.ForEach(p => db.Produkt.Add(p));
            db.SaveChanges();

        }


    }
}
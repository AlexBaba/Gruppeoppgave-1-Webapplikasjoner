using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Nettbutikk.DAL;

namespace Final_Nettbutikk.ViewModels
{
    public class ReceiptViewModel
    {

        public ReceiptViewModel()
        {

            Produkter = new List<Produkt>();
            Antall = new List<int>();
            Ordre = new Ordre();

            var db = new NettbutikkEntities();
            TotalPrice = 0;

            int cartSize = CookieHandler.getCartSize();

            for (int i = 0; i < cartSize; i++)
            {
                string current = Convert.ToString(i);
                string[] values = CookieHandler.getCookieValues(CookieHandler.getCookie(CookieHandler.PRODUCT + current));

                if (values[0] == null || values[1] == null)
                {
                    //Throw some error
                    continue;
                }

                Produkter.Add(db.Produkt.Find(Convert.ToInt32(values[0])));
                Antall.Add(Convert.ToInt32(values[1]));

            }

            foreach (Produkt p in Produkter) {
                TotalPrice += p.Pris;
            }

            string email = HttpContext.Current.Session["User"].ToString();

            Person user = (from p in db.Person where p.Email == email select p).FirstOrDefault();

            if (user == null)
            {
                //Could not find a user with that email
                return;
            }

            Buyer = (from k in db.Kunde where k.PersonId == user.PersonId select k).FirstOrDefault();

            if (Buyer == null)
            {
                //The user is not registered to buy products
                return;
            }

            Ordre.OrdreId = db.Ordre.ToArray().Length;
            Ordre.KundeId = Buyer.KundeId;
            Ordre.Dato = DateTime.Now;

        }

        public List<Produkt> Produkter { get; set; }
        public List<int> Antall { get; set; }
        public double TotalPrice { get; set; }
        public Kunde Buyer { get; set; }
        public Ordre Ordre { get; set; }
    }
}
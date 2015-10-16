using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Nettbutikk.DAL;

namespace Final_Nettbutikk.ViewModels
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel() {

            Produkter = new List<Produkt>();
            Bilder = new List<Bilde>();
            Antall = new List<int>();
            TotalPrice = 0;

            var db = new NettbutikkEntities();

            CartSize = CookieHandler.getCartSize();

            for (int i = 0; i < CartSize; i++)
            {
                string current = Convert.ToString(i);
                HttpCookie productCookie = CookieHandler.getCookie(CookieHandler.PRODUCT + current);

                string[] values = CookieHandler.getCookieValues(productCookie);

                if (values[0] == null || values[1] == null)
                {
                    continue;
                }

                Produkt cartProduct = db.Produkt.Find(Convert.ToInt32(values[0]));
                Bilde productPicture = (from b in db.Bilde where b.ProduktId == cartProduct.ProduktId select b).FirstOrDefault();
                int productQuantity = Convert.ToInt32(values[1]);

                Produkter.Add(cartProduct);
                Bilder.Add(productPicture);
                Antall.Add(productQuantity);

                TotalPrice += cartProduct.Pris * productQuantity;
            }



        }

        public int CartSize { get; set; }
        public double TotalPrice { get; set; }
        public List<Produkt> Produkter { get; set; }
        public List<int> Antall { get; set; }
        public List<Bilde> Bilder { get; set; }

    }
}
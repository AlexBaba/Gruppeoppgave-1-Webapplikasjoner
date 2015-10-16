using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.Models.Binding
{
    using Models;
    using DataAccess;
    using Infrastructure;

    public class Checkout
    {
        public Checkout()
        {

            Produkter = new List<Product>();
            Bilder = new List<Image>();
            Antall = new List<int>();
            TotalPrice = 0;

            var db = new NettbutikkContext();

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

                Product cartProduct = db.Products.Find(Convert.ToInt32(values[0]));
                Image productPicture = (from image in db.Images where image.Product.Id == cartProduct.Id select image).FirstOrDefault();
                int productQuantity = Convert.ToInt32(values[1]);

                Produkter.Add(cartProduct);
                Bilder.Add(productPicture);
                Antall.Add(productQuantity);

                TotalPrice += cartProduct.Price * productQuantity;
            }



        }

        public int CartSize { get; set; }
        public double TotalPrice { get; set; }
        public List<Product> Produkter { get; set; }
        public List<int> Antall { get; set; }
        public List<Image> Bilder { get; set; }

    }
}
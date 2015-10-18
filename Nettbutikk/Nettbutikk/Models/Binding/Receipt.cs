using Nettbutikk.DataAccess;
using Nettbutikk.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.Models.Binding
{
    public class ReceiptViewModel
    {
        public ReceiptViewModel()
        {

            Products = new List<Product>();
            Amounts = new List<int>();
            Order = new Order();

            var db = new NettbutikkContext();
            
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

                Products.Add(db.Products.Find(Convert.ToInt32(values[0])));
                Amounts.Add(Convert.ToInt32(values[1]));

            }
            
            string email = HttpContext.Current.Session["User"].ToString();
            
            Buyer = (from u in db.Users where u.Email == email select u).FirstOrDefault();

            if (Buyer == null)
            {
                //The user is not registered to buy products
                return;
            }
            
            Order.Customer = Buyer;
            Order.PlacementDateTime = DateTime.Now;

        }

        public List<Product> Products { get; set; }
        public List<int> Amounts { get; set; }
        public double TotalPrice
        {
            get {
                return Products
                    .Zip(Amounts, (product, amount) => {
                        return product.Price * amount;
                    })
                    .Sum();
            }
        }
        public User Buyer { get; set; }
        public Order Order { get; set; }
        public object Message { get; internal set; }
    }
}
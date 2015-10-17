using System.Linq;
using System.Web.Mvc;
using Nettbutikk.Models.Binding;
using Nettbutikk.Infrastructure;
using Nettbutikk.Models;
using System.Web;
using System;

namespace Nettbutikk.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Search(string term)
        {
            return View(db.Products.Where((product) => product.Description.Contains(term)).ToList());
        }

        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Products = db.Products.ToList();
            return View();
        }
        
        public ActionResult Checkout()
        {
            Checkout viewModel = new Checkout();
            return View(viewModel);
        }

        [HttpPost]
        public int AddToCart(int ProductId)
        {
            var cookie = Request.Cookies["Shoppingcart"] ?? new HttpCookie("Shoppingcart");
            int numProduct;
            try
            {
                numProduct = Convert.ToInt32(cookie[ProductId.ToString()]);
                numProduct++;
            }
            catch (Exception)
            {
                numProduct = 1;
            }
            cookie[ProductId.ToString()] = numProduct.ToString();
            Response.Cookies.Add(cookie);

            var list = cookie.Values;
            var numItemsInCart = 0;
            foreach (var c in list)
            {
                var count = Convert.ToInt32(cookie[c.ToString()]);
                numItemsInCart += count;
            }
            return numItemsInCart;
        }

        public ActionResult Receipt()
        {

            if (CookieHandler.getCartSize() < 1)
            {
                //return View("Error");
            }
            else if (Session["User"] == null)
            {
                System.Diagnostics.Debug.WriteLine("No user found!");
                Session["User"] = "alex.gaard@hotmail.com";
                //return View("Error");
            }

            System.Diagnostics.Debug.WriteLine("Logged in as: " + Session["User"].ToString());

            ReceiptViewModel viewModel = new ReceiptViewModel();

            db.Orders.Add(viewModel.Order);
            db.SaveChanges();

            int bestillingId = db.Orders.ToArray().Count();
            int bestillinger = viewModel.Products.Count;

            for (int i = 0; i < bestillinger; i++)
            {
                OrderLine line = new OrderLine();
                line.Order = viewModel.Order;
                line.Product = viewModel.Products[i];
                line.Amount = viewModel.Amounts[i];
                db.OrderLines.Add(line);
            }

            db.SaveChanges();

            /*
            //Remove all elements from the cart
            for (int i = 1; i <= elements; i++)
            {
                string current = Convert.ToString(i);
                System.Diagnostics.Debug.WriteLine("Deleting cookie: " + current);
                CookieHandler.deleteCookie(CookieHandler.getCookie(CookieHandler.PRODUCT + current));
            }

            System.Diagnostics.Debug.WriteLine("Delete cart size cookie");
            CookieHandler.deleteCookie(cartSizeCookie);
            */

            return View(viewModel);
        }

    }
}

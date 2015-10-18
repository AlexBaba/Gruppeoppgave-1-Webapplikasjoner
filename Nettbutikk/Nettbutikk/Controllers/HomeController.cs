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
            var cookie = Request.Cookies[CookieHandler.SHOPPING_CART_COOKIE.Name] ?? CookieHandler.SHOPPING_CART_COOKIE;
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

            ReceiptViewModel viewModel = new ReceiptViewModel();

            if (viewModel.Message != null) {
                return View(viewModel);
            }

            var order = db.Orders.Add(viewModel.Order);
            db.SaveChanges();

            var lines = viewModel.Products.Zip(viewModel.Amounts,
                (product, amount) =>
                    new OrderLine()
                    {
                        Order = viewModel.Order,
                        Product = product,
                        Amount = amount
                    });

            foreach (var line in lines)
            {
                db.OrderLines.Add(line);
            }

            db.SaveChanges();

            CookieHandler.deleteCookie(CookieHandler.SHOPPING_CART_COOKIE);

            return View(viewModel);
        }

    }
}

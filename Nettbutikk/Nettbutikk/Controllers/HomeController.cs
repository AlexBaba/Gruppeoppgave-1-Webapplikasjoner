
using Newtonsoft.Json;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikk.Infrastructure;

namespace Nettbutikk.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Products = db.Products.ToList();

            ViewBag.LoggedIn = LoginStatus();
            //ViewBag.Category = 2;
            ViewBag.CategoryName = "Default";

            return View();
        }

        public ActionResult Category(int CategoryID)
        {
            ViewBag.Categories = db.Categories.AsEnumerable();
            ViewBag.Products = db.Products.AsEnumerable();
            
            ViewBag.LoggedIn = LoginStatus();

            var category = db.Categories.Find(CategoryID);
            
            if(null == category)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryName = category.Name;

            return View("Index");
        }

        [HttpPost]
        public int AddToCart(int ProductId)
        {
            int numProduct;
            try
            {
                numProduct = Convert.ToInt32(Cart[ProductId.ToString()]);
                numProduct++;
            }
            catch (Exception)
            {
                numProduct = 1;
            }
            Cart[ProductId.ToString()] = numProduct.ToString();
            Response.Cookies.Add(Cart);

            var total = 0;

            foreach (var c in Cart.Values)
            {
                try
                {
                    total += Convert.ToInt32(Cart[c.ToString()]);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return total;
        }

        public int NumItemsInCart()
        {
            var total = 0;

            foreach (var c in Cart.Values)
            {
                try
                {

                    var count = Convert.ToInt32(Cart[c.ToString()]);
                    total += count;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return total;
        }

        public string GetCartJson()
        {
            var cart = new ShoppingCart();

            foreach (var item in Cart.Values)
            {
                try
                {
                    cart.Items.Add(new CartItem
                    {
                        Product = db.Products.Find(Convert.ToInt32(item)),
                        Amount = Convert.ToUInt32(Cart.Values[item.ToString()])
                    });

                }
                catch (Exception)
                {
                    continue;
                }
            }

            return JsonConvert.SerializeObject(cart);
        }

        public ActionResult ShoppingCart(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.ShoppingCart = GetCartList();
            ViewBag.LoggedIn = LoginStatus();
            return View();
        }

        private List<CartItem> GetCartList()
        {
            var cart = new List<CartItem>();

            foreach (var c in Cart.Values)
            {
                try
                {
                    var product = db.Products.Find(Convert.ToInt32(c));
                    cart.Add(new CartItem
                    {
                        Product = product,
                        Amount = Convert.ToUInt32(Cart[c.ToString()])
                    });
                }
                catch (Exception)
                {
                    continue;
                }

            }

            return cart;
        }

        [HttpPost]
        public int RemoveFromCart(int ProductId)
        {
            Cart.Values[ProductId.ToString()] = null;

            Response.AppendCookie(Cart);

            GetCartJson();

            return Cart.Values.Count;
        }

        public double GetSumTotalCart()
        {
            var sumTotal = 0.0;
            var cart = GetCartList();

            foreach (var item in cart)
            {
                sumTotal += item.Product.Price * item.Amount;
            }

            return sumTotal;
        }

        public int UpdateCartProductCount(int ProductId, int Count)
        {
            var cookie = Request.Cookies["Shoppingcart"];
            cookie[ProductId.ToString()] = Count.ToString();
            Response.AppendCookie(cookie);

            return NumItemsInCart();

        }

        public ActionResult EmptyCart(string returnUrl)
        {
            var cookie = Request.Cookies["Shoppingcart"];
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);
        }
    }
}
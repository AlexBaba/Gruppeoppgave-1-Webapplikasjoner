using Nettbutikk.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Product(int ProductId,string ReturnUrl)
        {
            var product = DB.GetProductById(ProductId);
            ViewBag.Product = product;
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.LoggedIn = LoginStatus();
            return View();
        }
    }
}
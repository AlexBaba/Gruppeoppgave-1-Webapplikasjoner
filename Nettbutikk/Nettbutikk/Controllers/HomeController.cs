using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikk.DataAccess;
using Nettbutikk.Models.Binding;

namespace Nettbutikk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Products = db.Products.ToList();
            return View();
        }
        
        public ActionResult Checkout()
        {
            CheckoutViewModel viewModel = new CheckoutViewModel();
            return View(viewModel);
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
            var db = new NettbutikkEntities();

            db.Ordre.Add(viewModel.Ordre);
            db.SaveChanges();

            int bestillingId = db.Bestilling.ToArray().Count();
            int bestillinger = viewModel.Produkter.Count;

            for (int i = 0; i < bestillinger; i++)
            {
                Bestilling produktBestilling = new Bestilling();
                produktBestilling.BestillingId = bestillingId++;
                produktBestilling.OrdreId = viewModel.Ordre.OrdreId;
                produktBestilling.ProduktId = viewModel.Produkter[i].ProduktId;
                produktBestilling.Antall = (short)viewModel.Antall[i];
                db.Bestilling.Add(produktBestilling);
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

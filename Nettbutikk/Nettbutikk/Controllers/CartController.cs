using Nettbutikk.Models;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class CartController : BaseController
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View(ShoppingCart.GetCart(this));
        }

        [HttpPost]
        public ActionResult Add(int? productId, uint? amount)
        {
            ShoppingCart.GetCart(this).Add(db.Products.Find(productId), amount ?? 1);
            return View("Edit");
        }

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // POST: ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                db.Carts.Remove(ShoppingCart.GetCart(this));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

using Nettbutikk.Models;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View(Cart.GetCart(this));
        }

        [HttpPost]
        public ActionResult Add(int? productId, uint? amount)
        {
            Cart.GetCart(this).Add(db.Products.Find(productId), amount ?? 1);
            return View("Edit");
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
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
        
        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                db.Carts.Remove(Cart.GetCart(this));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

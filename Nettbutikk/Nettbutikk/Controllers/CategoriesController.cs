using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Nettbutikk.Models;
using System.Linq;

namespace Nettbutikk.Controllers
{
    public class CategoriesController : BaseController
    {
        // GET: Categories
        public async Task<ActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(string name)
        {
            if (name == null || name.Length < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = await db.Categories.FindAsync(name);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: category/{id} (products in category)
        public async Task<ActionResult> Products(int id)
        {
            return View(await db.Products.Where(p => id == p.Category.Id).ToListAsync());
        }
    }
}

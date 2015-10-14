using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nettbutikk.DataAccessLayer;
using Nettbutikk.Models;

namespace Nettbutikk.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: Products/{id:Guid}
        public async Task<ActionResult> DetailsId(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/{name:string}
        public async Task<ActionResult> DetailsName(string name)
        {
            if (name == null || name.Length < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await db.Products.Where(p => p.Name == name).FirstAsync();

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }
    }
}

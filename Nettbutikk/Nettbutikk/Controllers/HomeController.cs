using System.Linq;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Products = db.Products.ToList();
            return View();
        }
    }
}
using Nettbutikk.Models.Binding;
using System.Web.Mvc;
using System.Linq;

namespace Nettbutikk.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            // Only show main-categories, not subcategories.
            return View(db.Categories.Where(c => c.ParentCategory == null).AsEnumerable());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(Search search)
        {
            return View();
        }
    }
}
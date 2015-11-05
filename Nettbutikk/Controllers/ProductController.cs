using System.Web.Mvc;
using Nettbutikk.BusinessLogic;
using Nettbutikk.Models;

namespace Nettbutikk.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController()
            : base()
        {

        }

        public ProductController(ServiceManager services)
            : base(services)
        {
        }

        // GET: Product
        public ActionResult Product(int? Id, string ReturnUrl)
        {
            if (null == Id)
                return HttpBadRequest();

            var product = Services.Products.GetById<ProductView>(Id);

            if(null == product)
            {
                return new HttpNotFoundResult();
            }
            
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.LoggedIn = LoginStatus();

            return View(product);
        }
    }
}
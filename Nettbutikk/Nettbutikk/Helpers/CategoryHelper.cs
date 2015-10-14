using Nettbutikk.Controllers;
using Nettbutikk.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nettbutikk.Helpers
{
    public class CategoryHelper
    {
        public static IEnumerable<Category> MainCategories(ViewContext vc)
        {
            return ((BaseController) vc.Controller).Categories();
        }
    }
}
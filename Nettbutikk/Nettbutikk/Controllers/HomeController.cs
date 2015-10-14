using Nettbutikk.Models;
using Nettbutikk.Models.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
﻿using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Categories = db.Categories.AsEnumerable();
            return View();
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
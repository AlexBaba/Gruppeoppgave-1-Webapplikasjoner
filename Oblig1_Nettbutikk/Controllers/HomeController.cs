﻿
using Newtonsoft.Json;
using Oblig1_Nettbutikk.BLL;
using Oblig1_Nettbutikk.Model;
using Oblig1_Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Category;

namespace Oblig1_Nettbutikk.Controllers
{
    public class HomeController : Controller
    {
        private IProductLogic _productBLL;
        private ICategoryLogic _categoryBLL;

        public HomeController()
        {
            _productBLL = new ProductBLL();
            _categoryBLL = new CategoryBLL();
        }

        public HomeController(IProductLogic productStub, ICategoryLogic categoryStub)
        {
            _productBLL = productStub;
            _categoryBLL = categoryStub;
        }

        public ActionResult Index()
        {

            var categories = _categoryBLL.GetAllCategoryModels().Select(c => new CategoryView()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }
            ).ToList();

            var products = _productBLL.GetProductsByCategory(1).Select( p => new ProductView()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryName = p.CategoryName
            }).ToList();

            ViewBag.Categories = categories ?? new List<CategoryView>();
            ViewBag.Products = products ?? new List<ProductView>();
            ViewBag.LoggedIn = LoginStatus();
            ViewBag.CategoryName = _categoryBLL.GetCategoryName(1);

            return View();
        }

        public ActionResult Category(int CategoryId)
        {
            var categories = _categoryBLL.GetAllCategoryModels().Select(c => new CategoryView()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }
            ).ToList();

            var products = _productBLL.GetProductsByCategory(CategoryId).Select(p => new ProductView()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryName = categories.FirstOrDefault(c => c.CategoryId == p.CategoryId).CategoryName
            }).ToList(); 

            ViewBag.Categories = categories;
            ViewBag.Products = products;
            ViewBag.LoggedIn = LoginStatus();
            ViewBag.CategoryName = _categoryBLL.GetCategoryName(CategoryId) ?? "Epler?";

            return View("Index");
        }


        public bool LoginStatus()
        {
            bool LoggedIn = false;
            if (Session["LoggedIn"] != null)
            {
                LoggedIn = (bool)Session["LoggedIn"];
            }
            return LoggedIn;
        }

    }
}
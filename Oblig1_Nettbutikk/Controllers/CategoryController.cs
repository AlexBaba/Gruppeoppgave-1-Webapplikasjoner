﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oblig1_Nettbutikk.Model;
using BLL.Category;
using Logging;

namespace Oblig1_Nettbutikk.Controllers
{
    public class CategoryController : Controller
    {

        private ICategoryLogic categoryBLL;
      

        public CategoryController()
        {

            categoryBLL = new CategoryBLL();
      
        }

        public CategoryController(ICategoryLogic categoryBLL)
        {

            this.categoryBLL = categoryBLL;
        

        }


        // GET: Image
        public ActionResult Index()
        {

            List<Category> allCategories = categoryBLL.GetAllCategories();

            ViewBag.Categories = allCategories;

            return View("ListCategory");
        }

        [HttpPost]
        public ActionResult Create(string Name)
        {

            if (Session["Admin"] != null && (bool)Session["Admin"] == false)
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Only administrators can create categories";
                return View("~/Views/Shared/Result.cshtml");
            }

            if (!categoryBLL.AddCategory(Name))
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Could not add the category to the database";
                return View("~/Views/Shared/Result.cshtml");
            }


            ViewBag.Title = "Success";
            ViewBag.Message = "Category was added to the database";
            return View("~/Views/Shared/Result.cshtml");
        }


        [HttpPost]
        public ActionResult Edit(string CategoryId,  string Name)
        {

            if (Session["Admin"] != null && (bool)Session["Admin"] == false)
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Only administrators can edit categories";
                return View("~/Views/Shared/Result.cshtml");
            }

            int categoryId;

            try
            {
                categoryId = Convert.ToInt32(CategoryId);
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                ViewBag.Title = "Error";
                ViewBag.Message = "Invalid category id: " + CategoryId;
                return View("~/Views/Shared/Result.cshtml");
            }

            if (!categoryBLL.UpdateCategory(categoryId, Name))
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Could not update the category";
                return View("~/Views/Shared/Result.cshtml");
            }

            ViewBag.Title = "Success";
            ViewBag.Message = "Category was updated";
            return View("~/Views/Shared/Result.cshtml");
        }

        public ActionResult Delete(string CategoryId)
        {

            if (Session["Admin"] != null && (bool)Session["Admin"] == false)
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Only administrators can delete categories";
                return View("~/Views/Shared/Result.cshtml");
            }


            int categoryId;

            try
            {
                categoryId = Convert.ToInt32(CategoryId);
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                ViewBag.Title = "Error";
                ViewBag.Message = "Invalid category id: " + CategoryId;
                return View("~/Views/Shared/Result.cshtml");
            }

            if (!categoryBLL.DeleteCategory(categoryId))
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Could not delete the category";
                return View("~/Views/Shared/Result.cshtml");
            }


            ViewBag.Title = "Success";
            ViewBag.Message = "Category was deleted";
            return View("~/Views/Shared/Result.cshtml");
        }


        public ActionResult CreateCategory()
        {

            return View();
        }



        public ActionResult EditCategory(string categoryId)
        {

            int nCategoryId;

            try
            {
                nCategoryId = Convert.ToInt32(categoryId);
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                ViewBag.Title = "Error";
                ViewBag.Message = "Invalid category id: " + categoryId;
                return View("~/Views/Shared/Result.cshtml");
            }

            Category category = categoryBLL.GetCategory(nCategoryId);

            if (category == null)
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Couldnt find a category with id: " + categoryId;
                return View("~/Views/Shared/Result.cshtml");
            }

            ViewBag.Category = category;

            return View();
        }


        public ActionResult DeleteCategory(string CategoryId)
        {

            int nCategoryId;

            try
            {
                nCategoryId = Convert.ToInt32(CategoryId);
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                ViewBag.Title = "Error";
                ViewBag.Message = "Invalid category id: " + CategoryId;
                return View("~/Views/Shared/Result.cshtml");
            }

            Category category = categoryBLL.GetCategory(nCategoryId);

            if (category == null)
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "Couldnt find a category with id: " + CategoryId;
                return View("~/Views/Shared/Result.cshtml");
            }

            ViewBag.Category = category;

            return View();
        }

    }
}
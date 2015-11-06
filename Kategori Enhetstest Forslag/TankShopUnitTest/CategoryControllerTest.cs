using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Category;
using BLL.Category;
using Oblig1_Nettbutikk.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Oblig1_Nettbutikk.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Oblig1_Nettbutikk.Model;
using Oblig1_Nettbutikk.Models;
using Oblig1_Nettbutikk.DAL;
using Oblig1_Nettbutikk.BLL;

namespace TankShopUnitTest
{
    [TestClass]
    public class CategoryControllerTest
    {
        //GoodInput = input of correct type and value
        //BadInput = input of wrong type. For example "abc123" being sent instead of an int
        //Invalid = input of correct type, but wrong value. For example a product id of -1


        [TestMethod]
        public void Index()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            var expectedResults = new List<Category> {
                new Category { CategoryId = 1, Name = "test1"},
                new Category { CategoryId = 2, Name = "test2"},
                new Category { CategoryId = 3, Name = "test3"},
                new Category { CategoryId = 4, Name = "test4"}
            };


            //Act
            var viewResult = controller.Index() as ViewResult;
            var actualResults = controller.ViewBag.Categories;

            //Assert

            Assert.AreEqual("ListCategory", viewResult.ViewName);
        }

        [TestMethod]
        public void CreateCategory()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));

            var expectedCategory = new Category { CategoryId = 1, Name = "test" };
            var SessionMock = new TestControllerBuilder();
            var Controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Admin"] = true;
            var CategoryId = 1;

            //Act
            var viewResult = controller.CreateCategory(CategoryId) as ViewResult;

            //Assert

            Assert.AreEqual("", viewResult.ViewName);
        }


        [TestMethod]
        public void EditCategoryGoodInput()
        {

            //Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()),
            SessionMock.InitializeController(Controller);
            var expectedCategory = new Category { CategoryId = 1, Name = "test" };
            string goodInput = "1";

            //Act
            var viewResult = controller.EditCategory(goodInput) as ViewResult;
            var actualCategory = controller.ViewBag.Category;
           

            //Assert
            Assert.AreEqual(expectedCategory.CategoryId, actualCategory.CategoryId);
            Assert.AreEqual(expectedCategory.CategoryId, actualCategory.CategoryId);
            Assert.AreEqual(expectedImage.CategoryId, actualCategory.CategoryId);
            Assert.AreEqual("", viewResult.ViewName);
        }


        [TestMethod]
        public void EditCategoryBadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string badInput = "bad input";

            //Act
            var viewResult = controller.EditCategory(badInput) as ViewResult;


            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + badInput, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }


        [TestMethod]
        public void EditCategoryNoImageFound()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string badCategoryId = "-1";

            //Act
            var viewResult = controller.EditCategory(badCategoryId) as ViewResult;


            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Couldnt find an image with id: " + badCategoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }


        [TestMethod]
        public void DeleteCategoryGoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string CategoryId = "2";

            Category expectedResult = new Category { CategoryId = 2, Name = "test" };

            //Act
            var viewResult = controller.DeleteCategory(categoryId) as ViewResult;
            var actualResult = controller.ViewBag.Category;

            //Assert
            Assert.AreEqual(expectedResult.CategoryId, actualResult.CategoryId);
            Assert.AreEqual(expectedResult.CategoryId, actualResult.CategoryId);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);

            Assert.AreEqual("", viewResult.ViewName);
        }

        [TestMethod]
        public void DeleteCategoryBadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string CategoryId = "bad input";

            //Act
            var viewResult = controller.DeleteCategory(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void DeleteCategoryNoCategoryFound()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "-1";

            //Act
            var viewResult = controller.DeleteCategory(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Could find an image with the id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }


        [TestMethod]
        public void CreateGoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));   
            string name = "url";

            //Act
            var viewResult = controller.Create(name) as ViewResult;

            //Assert
            Assert.AreEqual("Success", controller.ViewBag.Title);
            Assert.AreEqual("Category was added to the database", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void CreateBadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string name = "url";

            //Act
            var viewResult = controller.Create(name) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid product id", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

    

        [TestMethod]
        public void EditGoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "1";
            string name = "url";

            //Act
            var viewResult = controller.Edit(categoryId, name) as ViewResult;

            //Assert
            Assert.AreEqual("Success", controller.ViewBag.Title);
            Assert.AreEqual("Category was updated", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }


        [TestMethod]
        public void EditBadCategoryId()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "bad";
            string name = "url";

            //Act
            var viewResult = controller.Edit(categoryId, name) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }



        [TestMethod]
        public void EditInvalidCategoryId()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "-1";
            string name = "url";

            //Act
            var viewResult = controller.Edit(categoryId, name) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Could not update the category", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }


        [TestMethod]
        public void DeleteGoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "1";

            Image expectedResult = new Category { CategoryId = 1, Name = "test" };

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Success", controller.ViewBag.Title);
            Assert.AreEqual("Category was deleted", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void DeleteBadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "bad input";

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void DeleteInvalidInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "-1";

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Could not delete the category", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

    }
}


        





    

   

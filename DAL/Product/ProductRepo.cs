﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblig1_Nettbutikk.Model;
using Logging;

namespace Oblig1_Nettbutikk.DAL
{
    public class ProductRepo : IProductRepo
    {
        public bool AddProduct(string Name, double Price, int Stock, string Description, string ImageUrl, int CategoryId)
        {
            try
            {
                var db = new TankshopDbContext();
                db.Products.Add(new Product() { Name = Name, Price = Price, Stock = Stock, Description = Description, ImageUrl = ImageUrl, CategoryId = CategoryId });
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }

        public bool DeleteProduct(int ProductId)
        {
            var db = new TankshopDbContext();

            Product product = (from p in db.Products where p.ProductId == ProductId select p).FirstOrDefault();

            if (product == null)
                return false;
            

            try
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }


        public bool UpdateProduct(int ProductId, string Name, double Price, int Stock, string Description, string ImageUrl, int CategoryId)
        {
            var db = new TankshopDbContext();

            Product product = (from p in db.Products where p.ProductId == ProductId select p).FirstOrDefault();

            if (product== null)
                return false;


            product.Name = Name;
            product.Price = Price;
            product.Stock = Stock;
            product.Description = Description;
            product.ImageUrl = ImageUrl;
            product.CategoryId = CategoryId;
           
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }

        /*
        public string GetCategoryName(int categoryId)
        {

            try {
                Category c = new TankshopDbContext().Categories.FirstOrDefault();
                if (c == null)
                    throw new Exception(); 
            }
            catch (Exception e) {

            }
            using (var db = new TankshopDbContext())
            {
                return db.Categories.Find(categoryId).Name;
            }
        }
        */

        public List<Product> GetAllProducts() {

            try
            {
                return new TankshopDbContext().Products.ToList();
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                return new List<Product>();
            }

        }

        public Product GetProduct(int ProductId) {

            try
            {
                return new TankshopDbContext().Products.Find(ProductId);
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                return null;
            }

        }

        public ProductModel GetProductModel(int ProductId)
        {
            using (var db = new TankshopDbContext())
            {
                var product = db.Products.Find(ProductId);
                var productModel = new ProductModel()
                {
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Stock = product.Stock
                };
                return productModel;
            }
        }

        public List<ProductModel> GetProducts(List<int> productIdList)
        {
            var productList = new List<ProductModel>();
            try
            {
                using (var db = new TankshopDbContext())
                {

                    foreach (var productId in productIdList)
                    {
                        var product = db.Products.Find(productId);
                        if (product != null)
                        {
                            var productModel = new ProductModel()
                            {
                                ProductId = product.ProductId,
                                ProductName = product.Name,
                                Description = product.Description,
                                Price = product.Price,
                                Stock = product.Stock,
                                ImageUrl = product.ImageUrl,
                                CategoryId = product.CategoryId,
                                CategoryName = product.Category.Name
                            };

                            productList.Add(productModel);
                        }
                    }

                    return productList;
                }
            }
            catch (Exception)
            {
                return productList;
            }
        }

        public List<ProductModel> GetProductsByCategory(int categoryId)
        {
            using(var db = new TankshopDbContext())
            {
                var dbProducts = db.Products.Where(p => p.CategoryId == categoryId).ToList();
                var productModels = new List<ProductModel>();
                foreach(var product in dbProducts)
                {
                    productModels.Add(new ProductModel()
                    {
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category.Name,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        ProductName = product.Name,
                        Stock = product.Stock
                    });
                }
                return productModels;
                
            }
        }

        public bool AddOldProduct(string Name, double Price, int Stock, string Description, string ImageUrl, int CategoryId, int AdminId)
        {
            var db = new TankshopDbContext();
            OldProduct oldProduct = new OldProduct();

            oldProduct.Name = Name;
            oldProduct.Price = Price;
            oldProduct.Stock = Stock;
            oldProduct.Description = Description;
            oldProduct.ImageUrl = ImageUrl;
            oldProduct.CategoryId = CategoryId;

            oldProduct.AdminId = AdminId;
            oldProduct.Changed = DateTime.Now;

            db.OldProducts.Add(oldProduct);

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }
    }
}

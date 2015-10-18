using Nettbutikk.DataAccess;
using Nettbutikk.Models;
using Nettbutikk.Models.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.Infrastructure
{
    public class DB
    {
        public static bool AttemptLogin(AccountLogin model)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var existingUser = db.Users.FirstOrDefault(u => model.Email == u.Email && u.Cred.Password == PasswordHash(model.Password));

                    if (existingUser == null)
                        return false;

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Product GetProductById(int productId)
        {
            using (var db = new NettbutikkContext())
            {
                return db.Products.Find(productId);
            }
        }

        internal static List<Product> GetProductsByCategory(int categoryID)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    List<Product> AllProducts = db.Products.ToList();
                    List<Product> products = new List<Product>();

                    foreach (var product in AllProducts)
                        if (product.Category.Id == categoryID)
                            products.Add(product);

                    return products;
                }
                catch (Exception)
                {
                    return new List<Product>();
                    throw;
                }
            }
        }

        public static ZipCode GetPostal(string zipcode)
        {
            using (var db = new NettbutikkContext())
            {
                return db.ZipCodes.Find(zipcode);
            }
        }

        public static User GetCustomerByEmail(string email)
        {
            using (var db = new NettbutikkContext())
            {
                var customer = db.Users.Find(email);
                return customer;
            }
        }

        public static string GetCategoryName(int categoryID)
        {
            using (var db = new NettbutikkContext())
            {
                var category = db.Categories.Find(categoryID);

                return category.Name;

            }
        }

        public static bool UpdateCustomer(EditAccount info, string email)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var user = db.Users.Find(email);
                    user.FirstName = info.FirstName;
                    user.LastName = info.LastName;

                    foreach(var address in info.Addresses)
                    {
                        user.Addresses.Add(address);
                    }
                    
                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        internal static byte[] PasswordHash(string password)
        {
            byte[] inData, outData;
            var alg = System.Security.Cryptography.SHA256.Create();
            inData = System.Text.Encoding.Default.GetBytes(password);
            outData = alg.ComputeHash(inData);
            return outData;
        }

        public static bool RegisterCustomer(RegisterViewModel info)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var existingCustomer = db.Users.Find(info.Email);

                    if (existingCustomer != null)
                        return false;

                    var newCustomer = new User
                    {
                        Email = info.Email,
                        FirstName = info.FirstName,
                        LastName = info.LastName
                    };
                    
                    var newCustomerCredential = new User.Credential
                    {
                        User = GetCustomerByEmail(info.Email),
                        Password = PasswordHash(info.Password)
                    };

                    db.Users.Add(newCustomer);
                    //db.CustomerCredentials.Add(newCustomerCredential);

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}

// test
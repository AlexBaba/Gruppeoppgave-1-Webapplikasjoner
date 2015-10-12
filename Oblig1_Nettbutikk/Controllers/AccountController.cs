using Oblig1_Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oblig1_Nettbutikk.Controllers
{
    public class AccountController : Controller
    {


        [HttpPost]
        public bool Login(string email, string password)
        {
            var customerLogin = new CustomerLoginPartial
            {
                Email = email,
                Password = password
            };

            if (DB.AttemptLogin(customerLogin))
            {
                var customer = new WebShopModel().Customers.Find(email);
                var cookie = CreateCookie(customer, "Userinfo");

                Response.Cookies.Add(cookie);

                Session["LoggedIn"] = true;
                ViewBag.LoggedIn = true;
                return true;
            }
            else
            {
                Session["LoggedIn"] = false;
                ViewBag.LoggedIn = false;
            }
            return false;
        }

        public void Logout()
        {
            Session["LoggedIn"] = false;
            ViewBag.LoggedIn = false;

        }

        [HttpPost]
        public bool Register(CustomerRegisterPartial customer, string returnUrl)
        {
            if (DB.RegisterCustomer(customer))
            {
                var reg = new WebShopModel().Customers.Find(customer.Email);

                
                HttpCookie cookie = CreateCookie(reg,"Userinfo");

                Response.Cookies.Add(cookie);

                Session["LoggedIn"] = true;
                RedirectToAction("Index", "Home");
                return true;
            }
            return false;
        }

        private HttpCookie CreateCookie(Customer reg, string cookieName)
        {
            var cookie = new HttpCookie(cookieName);
            cookie["Firstname"] = reg.Firstname;
            cookie["Lastname"] = reg.Lastname;

            return cookie;
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
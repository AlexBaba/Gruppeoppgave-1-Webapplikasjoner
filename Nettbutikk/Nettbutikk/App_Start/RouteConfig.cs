using Nettbutikk.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nettbutikk
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "ProductDetails",
                url: "product/{id:Guid}",
                defaults: new {
                    controller = "ProductsController",
                    action = "Details",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Products",
                url: "products/{id}/{action}",
                defaults: new {
                    controller = "ProductsController",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Category",
                url: "category/{name:string}",
                defaults: new { controller = "CategoriesController", action = "show" }
            );

            routes.MapRoute(
                name: "Categories",
                url: "categories/{action}/{id}",
                defaults: new
                {
                    controller = "CategoriesController",
                    action = "Index"
                }
            );

            routes.MapRoute(
                "Search",
                url: "search/{term:string}",
                defaults: new {
                    controller = "Home",
                    action = "Search"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Admin/Products",
                url: "admin/products/{action}",
                defaults: new {
                    controller = "ProductsController",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}

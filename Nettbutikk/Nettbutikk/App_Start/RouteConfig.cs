using Nettbutikk.Infrastructure;
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
                name: "ProductDetailsById",
                url: "product/{id}",
                defaults: new
                {
                    controller = "Products",
                    action = "Details"
                }
            );
            
            routes.MapRoute(
                name: "ProductActions",
                url: "product/{id}/{action}",
                defaults: new {
                    controller = "Products"
                }
            );

            routes.MapRoute(
                name: "CategoryByName",
                url: "category/{id}",
                defaults: new { controller = "Categories", action = "Products" }
            );

            routes.MapRoute(
                name: "Categories",
                url: "category/{id}/{action}",
                defaults: new
                {
                    controller = "Categories"
                }
            );

            routes.MapRoute(
                name: "AccountManagement",
                url: "account/{action}",
                defaults: new
                {
                    controller = "Account.ManageController",
                    action = "Index"
                });

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
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}

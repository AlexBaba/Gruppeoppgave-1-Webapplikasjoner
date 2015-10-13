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
                url: "product/{id:Guid}",
                defaults: new
                {
                    controller = "Products",
                    action = "DetailsId"
                },
                constraints: new
                {
                    id = new GuidConstraint()
                }
            );

            routes.MapRoute(
                name: "ProductDetailsByName",
                url: "product/{name:string}",
                defaults: new
                {
                    controller = "Products",
                    action = "DetailsName"
                }
            );

            routes.MapRoute(
                name: "ProductActions",
                url: "product/{id:Guid}/{action}",
                defaults: new {
                    controller = "Products",
                    action = "Edit"
                },
                constraints: new
                {
                    id = new GuidConstraint()
                }
            );

            routes.MapRoute(
                name: "CategoryByName",
                url: "category/{name:string}",
                defaults: new { controller = "Categories", action = "ListProducts" }
            );

            routes.MapRoute(
                name: "Categories",
                url: "categories/{action}/{id}",
                defaults: new
                {
                    controller = "Categories",
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
                name: "Admin/Products",
                url: "admin/products/{action}/{id:Guid}",
                defaults: new {
                    controller = "Products",
                    action = "Index",
                    id = UrlParameter.Optional
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

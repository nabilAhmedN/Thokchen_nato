using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThokchenNato
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Account", "Account/{action}/{id}", new { controller = "Account", action = "Index", id = UrlParameter.Optional }, new[] { "ThokchenNato.Controllers" });
            routes.MapRoute("Product1", "Product/{action}/{id}", new { controller = "Product", action = "ViewAll", id = UrlParameter.Optional }, new[] { "ThokchenNato.Controllers" });
            routes.MapRoute("Category", "Category/{action}/{id}", new { controller = "Category", action = "ViewAll", id = UrlParameter.Optional }, new[] { "ThokchenNato.Controllers" });

            routes.MapRoute("Product", "tst", new { controller = "Product", action = "Index" }, new[] { "ThokchenNato.Controllers" });
            routes.MapRoute("Default", "", new { controller = "Product", action = "Index" }, new[] { "ThokchenNato.Controllers" });

            //routes.MapRoute("Cart", "Cart/{action}/{id}", new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, new[] { "ThokchenNato.Controllers" });

            //routes.MapRoute("Shop", "Shop/{action}/{name}", new { controller = "Shop", action = "Index", name = UrlParameter.Optional }, new[] { "ThokchenNato.Controllers" });

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Product", action = "ViewAll", id = UrlParameter.Optional }
            //);
        }
    }
}

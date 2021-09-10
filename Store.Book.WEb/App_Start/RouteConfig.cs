using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Store.Book.WEb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Store",
                url: "store/{year}/{month}/{day}/{title}",
                defaults: new { controller = "Store", action = "List", month = "1", day = "1" },
                    new { year = @"\d{2}|\d{4}", month = @"\d{1,2}", day = @"\d{1,2}" }
                    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

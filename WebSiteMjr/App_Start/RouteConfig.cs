using System.Web.Mvc;
using System.Web.Routing;

namespace WebSiteMjr
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MjrClient",
                url: "Account/Manage/{controller}/{action}/{id}",
                defaults: new { controller = "ClientHome", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RaceWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PageForUsers",
                url: "Index",
                defaults: new { controller = "Home", action = "Index", area = "" }
            );

            routes.MapRoute(
                name: "PageForRacerInfo",
                url: "Member/{SelectedRacerID}",
                defaults: new { controller = "Home", action = "RacerInfo", area = "", SelectedRacerID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", area = "", id = UrlParameter.Optional }
            );        
        }
    }
}

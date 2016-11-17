using RaceDataAccess;
using RaceLogic;
using RaceWebsite.App_Start;
using RaceWebsite.ClassesForRaceWebsite;
using RaceWebsite.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RaceWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
            });
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new FilterForIEUsers());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

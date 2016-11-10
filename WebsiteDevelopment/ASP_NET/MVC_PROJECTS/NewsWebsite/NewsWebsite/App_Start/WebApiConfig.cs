using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using NewsInfrastructure;

namespace NewsWebsite
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        { 
            config.MapHttpAttributeRoutes();

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<News>("GettingNewsUsingHeaderValue");
            builder.EntitySet<Comment>("Comments");
            config.Routes.MapODataServiceRoute("odata", "NewsOdata", builder.GetEdmModel());


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ); 
        }
    }
}

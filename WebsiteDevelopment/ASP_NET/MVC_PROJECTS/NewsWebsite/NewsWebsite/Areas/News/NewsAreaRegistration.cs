using System.Web.Mvc;

namespace NewsWebsite.Areas.News
{
    public class NewsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "News";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "News_default",
                "News/{controller}/{action}/{id}",
                new { controller = "News", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
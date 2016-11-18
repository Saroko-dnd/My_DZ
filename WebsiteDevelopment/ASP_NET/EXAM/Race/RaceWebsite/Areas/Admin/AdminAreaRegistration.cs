using System.Web.Mvc;

namespace RaceWebsite.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminMainPage",
                "Admin/Index",
                new { controller = "Admin", action = "Index", area = "Admin", id = UrlParameter.Optional }
            );  

            context.MapRoute(
               "AdminSelectRacerForEditing",
               "Admin/Member/{SelectedRacerID}",
               new { controller = "Admin", action = "EditRacerInfo", area = "Admin", SelectedRacerID = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Index", area = "Admin", id = UrlParameter.Optional }
            );
        }
    }
}
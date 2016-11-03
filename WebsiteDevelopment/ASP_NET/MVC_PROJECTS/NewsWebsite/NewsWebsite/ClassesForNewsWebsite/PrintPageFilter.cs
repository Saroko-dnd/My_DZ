using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public class PrintPageFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string UserWantPrintPage = filterContext.Controller.ValueProvider.GetValue("NewsPrintVersion").AttemptedValue;
            if (UserWantPrintPage == Resources.Texts.LinkToPrintVersion)
            {
                filterContext.RouteData.Values["action"] = "Index";
            }
        }
    }
}
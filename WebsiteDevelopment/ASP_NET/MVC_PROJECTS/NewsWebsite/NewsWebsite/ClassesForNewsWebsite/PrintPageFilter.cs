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
            ValueProviderResult FoundValue = filterContext.Controller.ValueProvider.GetValue("NewsPrintVersion");
            if (FoundValue != null && FoundValue.AttemptedValue == Resources.Texts.LinkToPrintVersion)
            {
                filterContext.RouteData.Values["action"] = "ShowNewsPrintVersion";
                filterContext.RouteData.Values["controller"] = "News";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public class FilterForPrintPage : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ValueProviderResult FoundValue = filterContext.Controller.ValueProvider.GetValue("NewsPrintVersion");
            if (FoundValue != null && FoundValue.AttemptedValue == Resources.Texts.LinkToPrintVersion)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "News" },
                        { "action", "ShowNewsPrintVersion" },
                        { "area", "News" },
                        { "SelectedNewsID",  filterContext.Controller.ValueProvider.GetValue("SelectedNewsID").AttemptedValue }
                    });
            }
        }
    }
}
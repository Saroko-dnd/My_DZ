using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public class FilterForNewsPartialSubmit : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IValueProvider CurrentValueProvider = filterContext.Controller.ValueProvider;
            //PropertyName  NewValue SelectedNewsID
            if (CurrentValueProvider.GetValue("PropertyName") != null && CurrentValueProvider.GetValue("NewValue") != null && CurrentValueProvider.GetValue("SelectedNewsID") != null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                                    { "controller", "Admin" },
                                    { "action", "SaveChangesInNewsAfterEdit" },
                                    { "area", "Admin" },
                                    { "PropertyName", CurrentValueProvider.GetValue("PropertyName").AttemptedValue },
                                    { "NewValue", CurrentValueProvider.GetValue("NewValue").AttemptedValue},
                                    { "SelectedNewsID", CurrentValueProvider.GetValue("SelectedNewsID").AttemptedValue }
                    });
            }
            else
            {
                filterContext.ActionParameters.Remove("PropertyName");
                filterContext.ActionParameters.Remove("NewValue");
            }
        }
    }
}
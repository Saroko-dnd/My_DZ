using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RaceWebsite.ClassesForRaceWebsite.Helpers
{
    public static class JSGenerator
    {
        public static MvcHtmlString JSResourcesObject(ResourceManager CurrentResourceManager)
        {
            StringBuilder JSStringBuilder = new StringBuilder();
            JSStringBuilder.Append("var Resources ={");
            JSStringBuilder.AppendLine();
            ResourceSet CurrentResourceSet = CurrentResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach(DictionaryEntry FoundResource in CurrentResourceSet)
            {
                JSStringBuilder.Append(FoundResource.Key.ToString() + ":'" + FoundResource.Value.ToString() + "',");
                JSStringBuilder.AppendLine();
            }
            JSStringBuilder.Append("};");

            return new MvcHtmlString(JSStringBuilder.ToString());
        }
    }
}
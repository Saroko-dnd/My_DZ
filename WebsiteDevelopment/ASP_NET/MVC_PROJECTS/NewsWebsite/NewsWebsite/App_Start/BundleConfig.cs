﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace NewsWebsite.App_Start
{
    public class BundleConfig
    {
        public static readonly string VirtualPathForJqueryBundle = "~/bundles/jquery";
        public static readonly string VirtualPathForEditableNewsBundle = "~/bundles/EditableNews";
        public static readonly string VirtualPathForSearchNewsSystemBundle = "~/bundles/SearchNewsSystem";
        public static readonly string VirtualPathForUnobtrusiveAjaxBundle = "~/bundles/jquery.unobtrusive-ajax";

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            string JqueryCdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js";
            bundles.Add(new ScriptBundle(VirtualPathForJqueryBundle, JqueryCdnPath).Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle(VirtualPathForUnobtrusiveAjaxBundle).Include("~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle(VirtualPathForEditableNewsBundle).Include("~/Scripts/jquery.jeditable.js").Include("~/Scripts/EditableNews.js"));
            bundles.Add(new ScriptBundle(VirtualPathForSearchNewsSystemBundle).Include("~/Areas/News/Scripts/SearchNewsSystem.js"));
            //BundleTable.EnableOptimizations = true;
        }
    }
}
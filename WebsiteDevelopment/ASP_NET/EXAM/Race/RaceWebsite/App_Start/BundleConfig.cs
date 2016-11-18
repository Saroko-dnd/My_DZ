using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RaceWebsite.App_Start
{
    public class BundleConfig
    {
        public static readonly string VirtualPathForJqueryBundle = "~/bundles/jquery";
        public static readonly string VirtualPathForChangingRacerInfoBundle = "~/bundles/ChangingRacerInfo";
        public static readonly string VirtualPathForUnobtrusiveAjaxBundle = "~/bundles/jquery.unobtrusive-ajax";
        public static readonly string VirtualPathForUpdatingRaceParticipantsInfoBundle = "~/bundles/UpdatingRaceParticipantsInfo";

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            string JqueryCdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js";
            bundles.Add(new ScriptBundle(VirtualPathForJqueryBundle, JqueryCdnPath).Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle(VirtualPathForChangingRacerInfoBundle).Include("~/Areas/Admin/Scripts/ChangingRacerInfo.js"));
            bundles.Add(new ScriptBundle(VirtualPathForUnobtrusiveAjaxBundle).Include("~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle(VirtualPathForUpdatingRaceParticipantsInfoBundle).Include("~/Scripts/UpdatingRaceParticipantsInfo.js"));
            //BundleTable.EnableOptimizations = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public static class ApplicationConstants
    {
        public static readonly string ConnectionStringName = "NewsWebsiteDB_NewVersion";
        public static readonly string PathFromRouteToNewsPartialView = "~/Areas/News/Views/Shared/NewsPartial.cshtml";
        public static readonly string PathFromRouteToCommentPartialView = "~/Areas/News/Views/Shared/CommentPartial.cshtml";

        public enum UI : long { Name, Family, ShortName, Age, Sex }
    }
}
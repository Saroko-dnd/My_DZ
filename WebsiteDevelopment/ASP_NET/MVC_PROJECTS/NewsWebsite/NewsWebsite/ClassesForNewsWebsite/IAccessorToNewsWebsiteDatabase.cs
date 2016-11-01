using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public interface IAccessorToNewsWebsiteDatabase
    {
        IEnumerable<News> GetImportantNews();
    }
}
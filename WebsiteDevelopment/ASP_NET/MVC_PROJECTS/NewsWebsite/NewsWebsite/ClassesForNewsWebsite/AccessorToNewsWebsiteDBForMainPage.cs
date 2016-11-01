using NewsDataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public class AccessorToNewsWebsiteDBForMainPage : IAccessorToNewsWebsiteDatabase
    {
        public IEnumerable<News> GetImportantNews()
        {
            List<News> CurrentCollectionOfImportantNews;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                CurrentCollectionOfImportantNews = TestDBContext.News.Where(CurrentNews => CurrentNews.HotNews == true).ToList();
            }
            return CurrentCollectionOfImportantNews;
        }
    }
}
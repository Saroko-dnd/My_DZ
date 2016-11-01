using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.Areas.News.Models
{
    public class PageWithListOfNews
    {
        private IEnumerable<NewsDataAccess.News> allNews;

        public IEnumerable<NewsDataAccess.News> AllNews
        {
            get { return allNews; }
            set { allNews = value; }
        }

        public PageWithListOfNews(IEnumerable<NewsDataAccess.News> ListOfNews)
        {
            AllNews = ListOfNews;
        }
    }
}
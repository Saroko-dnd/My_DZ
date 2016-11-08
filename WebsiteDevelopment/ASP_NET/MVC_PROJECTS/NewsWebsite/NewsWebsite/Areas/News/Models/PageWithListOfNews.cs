using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.Areas.News.Models
{
    public class PageWithListOfNews
    {
        private IEnumerable<NewsInfrastructure.News> allNews;
        private bool adminIsHere;

        public IEnumerable<NewsInfrastructure.News> AllNews
        {
            get { return allNews; }
            set { allNews = value; }
        }

        public bool AdminIsHere
        {
            get
            {
                return adminIsHere;
            }

            set
            {
                adminIsHere = value;
            }
        }

        public PageWithListOfNews(IEnumerable<NewsInfrastructure.News> ListOfNews)
        {
            AllNews = ListOfNews;
        }
    }
}
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
        public IEnumerable<Comment> GetAllCommentsForNews(News SelectedNews)
        {
            List<Comment> ListOfComments;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                ListOfComments = TestDBContext.Comments.Where(CurrentComment => CurrentComment.News == SelectedNews).ToList();
            }
            return ListOfComments;
        }

        public IEnumerable<News> GetAllNews()
        {
            List<News> NewsList;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                NewsList = TestDBContext.News.ToList();
            }
            return NewsList;
        }

        public IEnumerable<News> GetHotNews()
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
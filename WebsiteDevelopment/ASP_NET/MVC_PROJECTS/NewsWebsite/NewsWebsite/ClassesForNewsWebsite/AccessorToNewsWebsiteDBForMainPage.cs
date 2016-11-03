using NewsDataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

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

        public News GetNewsByID(long NewsIDForSearch)
        {
            News FoundNews = null;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                FoundNews = TestDBContext.News.Where(CurNews => CurNews.NewsID == NewsIDForSearch).FirstOrDefault();
                FoundNews.Comments = TestDBContext.Comments.Where(CurComment => CurComment.NewsID == FoundNews.NewsID).Include(FoundComment => FoundComment.LikesAndDislikes).
                    Include(ResComment => ResComment.Author).ToList();       
            }
            return FoundNews;
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


        public void AddNewNews(News NewNews)
        {
            NewNews.Date = DateTime.Now;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                TestDBContext.News.Add(NewNews);
                TestDBContext.SaveChanges();
            }
        }
    }
}
using NewsDataAccess;
using NewsInfrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Reflection;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public class AccessorToNewsWebsiteDBForMainPage : IAccessorToNewsWebsiteDatabase
    {      
        public IEnumerable<Comment> GetAllCommentsForNews(News SelectedNews)
        {
            List<Comment> ListOfComments;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                ListOfComments = TestDBContext.AllComments.Where(CurrentComment => CurrentComment.News == SelectedNews).ToList();
            }
            return ListOfComments;
        }

        public IEnumerable<News> GetAllNews()
        {
            List<News> NewsList;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                NewsList = TestDBContext.AllNews.ToList();
            }
            return NewsList;
        }

        public News GetNewsByID(long NewsIDForSearch)
        {
            News FoundNews = null;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                FoundNews = TestDBContext.AllNews.Where(CurNews => CurNews.NewsID == NewsIDForSearch).FirstOrDefault();
                FoundNews.Comments = TestDBContext.AllComments.Where(CurComment => CurComment.NewsID == FoundNews.NewsID).ToList();
                foreach (Comment CurrentComment in FoundNews.Comments)
                {
                    CurrentComment.Author = TestDBContext.AllUsers.Where(CurrentUser => CurrentUser.UserID == CurrentComment.AuthorID).FirstOrDefault();
                    CurrentComment.LikesAndDislikes = TestDBContext.AllLikesAndDislikes.Where(CurrentUserOpinion => CurrentUserOpinion.CommentID == CurrentComment.CommentID).ToList();
                }       
            }
            return FoundNews;
        }

        public IEnumerable<News> GetHotNews()
        {
            List<News> CurrentCollectionOfImportantNews;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                CurrentCollectionOfImportantNews = TestDBContext.AllNews.Where(CurrentNews => CurrentNews.HotNews == true).ToList();
            }
            return CurrentCollectionOfImportantNews;
        }


        public void AddNewNews(News NewNews)
        {
            NewNews.Date = DateTime.Now;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                TestDBContext.AddNews(NewNews);
                TestDBContext.SaveAllChangesMadeInsideCollections();
            }
        }

        public IEnumerable<News> GetNewsByType(Enums.NewsTypes SelectedTypeOfNews)
        {
            List<News> ListOfNewsWithSelectedType;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                ListOfNewsWithSelectedType = TestDBContext.AllNews.Where(FoundNews => FoundNews.Type == SelectedTypeOfNews).ToList();
            }
            return ListOfNewsWithSelectedType;
        }

        /// <summary>
        /// Sets a new value for the selected property of the News objec with selected id, saves changes in the database and returns modified News object.
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="CurrentNewsID"></param>
        /// <returns></returns>
        public News UpdateNewsProperty(string PropertyName, object PropertyValue, long CurrentNewsID)
        {
            News ChangedNews = null;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                ChangedNews = TestDBContext.AllNews.Where(CurrentNews => CurrentNews.NewsID == CurrentNewsID).FirstOrDefault();
                Type CurrentType = ChangedNews.GetType();
                PropertyInfo NewsProperty = CurrentType.GetProperty(PropertyName);
                NewsProperty.SetValue(ChangedNews, PropertyValue, null);
                TestDBContext.SaveAllChangesMadeInsideCollections();
            }
            return ChangedNews;
        }

    }
}
using NewsDataAccess;
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

        /// <summary>
        /// Sets a new value for the selected property of the News objec with selected id, saves changes in the database and returns modified News object.
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="CurrentNewsID"></param>
        /// <returns></returns>
        public News UpdateNewsProperty(string PropertyName, object PropertyValue, long CurrentNewsID)
        {
            News FoundNews = null;
            using (NewsWebsiteContext TestDBContext = new NewsWebsiteContext(ApplicationConstants.ConnectionStringName))
            {
                FoundNews = TestDBContext.News.Where(CurrentNews => CurrentNews.NewsID == CurrentNewsID).FirstOrDefault();
                if (FoundNews == null)
                {
                    throw new Exception(Resources.Texts.ExceptionCantFindNewsWithID + " = " + CurrentNewsID.ToString());
                }
                else
                {
                    Type CurrentType = FoundNews.GetType();
                    PropertyInfo NewsProperty = CurrentType.GetProperty(PropertyName);
                    if (NewsProperty == null)
                    {
                        throw new Exception(Resources.Texts.ExceptionNewsPropertyWithSuchNameDoesNotExist + " " + PropertyName);
                    }
                    else
                    {
                        NewsProperty.SetValue(FoundNews, PropertyValue, null);
                    }
                }
                TestDBContext.SaveChanges();
            }
            return FoundNews;
        }

    }
}
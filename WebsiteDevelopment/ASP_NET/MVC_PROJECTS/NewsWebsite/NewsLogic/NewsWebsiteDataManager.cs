using NewsInfrastructure;
using Ninject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SuperLinqExstensionMethods;

namespace NewsLogic
{
    public class NewsWebsiteDataManager : INewsWebsiteDataManager
    {
        private IKernel CurrentKernelWithRepositoryBinding;

        public IEnumerable<Comment> GetAllCommentsForNews(News SelectedNews)
        {
            List<Comment> ListOfComments;
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
            {
                ListOfComments = TestDBContext.AllComments.Where(CurrentComment => CurrentComment.News == SelectedNews).ToList();
            }
            return ListOfComments;
        }

        public IEnumerable<News> GetAllNews()
        {
            List<News> NewsList;
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
            {
                NewsList = TestDBContext.AllNews.ToList();
            }
            return NewsList;
        }

        public News GetNewsByID(long NewsIDForSearch)
        {
            News FoundNews = null;
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
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
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
            {
                CurrentCollectionOfImportantNews = TestDBContext.AllNews.Where(CurrentNews => CurrentNews.HotNews == true).ToList();
            }
            return CurrentCollectionOfImportantNews;
        }


        public void AddNewNews(News NewNews)
        {
            NewNews.Date = DateTime.Now;
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
            {
                TestDBContext.AddNews(NewNews);
                TestDBContext.SaveAllChangesMadeInsideCollections();
            }
        }

        public IEnumerable<News> GetNewsByType(Enums.NewsTypes SelectedTypeOfNews)
        {
            List<News> ListOfNewsWithSelectedType;
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
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
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
            {
                ChangedNews = TestDBContext.AllNews.Where(CurrentNews => CurrentNews.NewsID == CurrentNewsID).FirstOrDefault();
                Type CurrentType = ChangedNews.GetType();
                PropertyInfo NewsProperty = CurrentType.GetProperty(PropertyName);
                NewsProperty.SetValue(ChangedNews, PropertyValue, null);
                TestDBContext.SaveAllChangesMadeInsideCollections();
            }
            return ChangedNews;
        }

        public IEnumerable<News> GetDistinctNewsWithSimilarHeader(string HeaderForSearch)
        {
            List<News> FoundNews = null;
            using (INewsWebsiteRepository TestDBContext = CurrentKernelWithRepositoryBinding.Get<INewsWebsiteRepository>())
            {
                FoundNews = TestDBContext.AllNews.Where(CurrentNews => CurrentNews.Header.Contains(HeaderForSearch)).DistinctBy(ResultNews => ResultNews.Header).ToList();
            }
            return FoundNews;
        }

        public NewsWebsiteDataManager(IKernel NewKernelWithRepositoryBinding)
        {
            CurrentKernelWithRepositoryBinding = NewKernelWithRepositoryBinding;
        }
    }
}

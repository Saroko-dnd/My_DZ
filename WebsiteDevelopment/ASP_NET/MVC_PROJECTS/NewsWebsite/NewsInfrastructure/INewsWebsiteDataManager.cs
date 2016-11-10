using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsInfrastructure
{
    public interface INewsWebsiteDataManager
    {

        IEnumerable<Comment> GetAllCommentsForNews(News SelectedNews);

        IEnumerable<News> GetAllNews();

        News GetNewsByID(long NewsIDForSearch);

        IEnumerable<News> GetHotNews();

        void AddNewNews(News NewNews);

        IEnumerable<News> GetNewsByType(Enums.NewsTypes SelectedTypeOfNews);
        IEnumerable<News> GetDistinctNewsWithSimilarHeader(string HeaderForSearch);

        News UpdateNewsProperty(string PropertyName, object PropertyValue, long CurrentNewsID);

    }
}

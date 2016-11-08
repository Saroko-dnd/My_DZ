
using NewsInfrastructure;
using NewsWebsite.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public interface IAccessorToNewsWebsiteDatabase
    {
        IEnumerable<News> GetHotNews();
        IEnumerable<News> GetAllNews();
        IEnumerable<Comment> GetAllCommentsForNews(News SelectedNews);
        void AddNewNews(News NewNews);
        News GetNewsByID(long NewsIDForSearch);
        IEnumerable<News> GetNewsByType(NewsForEditInEditorTemplateModel.NewsTypes SelectedTypeOfNews);
        News UpdateNewsProperty(string PropertyName, object PropertyValue, long CurrentNewsID);
    }
}
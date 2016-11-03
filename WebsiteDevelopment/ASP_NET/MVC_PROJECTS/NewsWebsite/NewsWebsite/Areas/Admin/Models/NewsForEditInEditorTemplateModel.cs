using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.Areas.Admin.Models
{
    public class NewsForEditInEditorTemplateModel : NewsDataAccess.News
    {
        private NewsTypes currentTypeOfNews;

        public NewsTypes CurrentTypeOfNews
        {
            get
            {
                return currentTypeOfNews;
            }

            set
            {
                this.Type = (int)value;
                currentTypeOfNews = value;
            }
        }

        public enum NewsTypes : int { Science, Politics, Games, Sport, Business };

        public NewsForEditInEditorTemplateModel(NewsDataAccess.News BaseNewsData)
        {
            CurrentTypeOfNews = (NewsTypes)BaseNewsData.Type;
            Date = BaseNewsData.Date;
            Comments = BaseNewsData.Comments;
            HotNews = BaseNewsData.HotNews;
            NewsID = BaseNewsData.NewsID;
            Header = BaseNewsData.Header;
            Body = BaseNewsData.Body;
        }

        public NewsDataAccess.News GetPureNews()
        {
            return new NewsDataAccess.News(Date, Header, Body, HotNews, (int)CurrentTypeOfNews);
        }

        public NewsForEditInEditorTemplateModel()
        {

        }
    }
}
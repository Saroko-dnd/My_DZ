using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.Areas.Admin.Models
{
    public class NewsForEditInEditorTemplateModel : NewsInfrastructure.News
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

        public NewsForEditInEditorTemplateModel(NewsInfrastructure.News BaseNewsData)
        {
            CurrentTypeOfNews = (NewsTypes)BaseNewsData.Type;
            Date = BaseNewsData.Date;
            Comments = BaseNewsData.Comments;
            HotNews = BaseNewsData.HotNews;
            NewsID = BaseNewsData.NewsID;
            Header = BaseNewsData.Header;
            Body = BaseNewsData.Body;
        }

        public NewsInfrastructure.News GetPureNews()
        {
            return new NewsInfrastructure.News(Date, Header, Body, HotNews, (int)CurrentTypeOfNews);
        }

        public NewsForEditInEditorTemplateModel()
        {

        }
    }
}
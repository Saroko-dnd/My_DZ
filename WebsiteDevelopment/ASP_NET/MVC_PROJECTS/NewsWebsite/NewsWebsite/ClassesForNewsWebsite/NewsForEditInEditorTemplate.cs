using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public class NewsForEditInEditorTemplate : News
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

        public NewsForEditInEditorTemplate(News BaseNewsData)
        {
            CurrentTypeOfNews = (NewsTypes)BaseNewsData.Type;
            Date = BaseNewsData.Date;
            Comments = BaseNewsData.Comments;
            HotNews = BaseNewsData.HotNews;
            NewsID = BaseNewsData.NewsID;
            Header = BaseNewsData.Header;
            Body = BaseNewsData.Body;
        }

        public NewsForEditInEditorTemplate()
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsInfrastructure;

namespace NewsWebsite.Areas.News.Models
{
    public class NewsForPartialView : NewsInfrastructure.News
    {
        private Enums.NewsTypes typeOfNewsAsEnum;

        public Enums.NewsTypes TypeOfNewsAsEnum
        {
            get { return typeOfNewsAsEnum; }
            set { typeOfNewsAsEnum = value; }
        }
        private bool selected = false;
        private bool printView = false;

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public bool PrintView
        {
            get
            {
                return printView;
            }

            set
            {
                printView = value;
            }
        }

        public NewsForPartialView(NewsInfrastructure.News BaseNewsData)
        {
            Date = BaseNewsData.Date;
            Type = BaseNewsData.Type;
            Comments = BaseNewsData.Comments;
            HotNews = BaseNewsData.HotNews;
            NewsID = BaseNewsData.NewsID;
            Header = BaseNewsData.Header;
            Body = BaseNewsData.Body;
            TypeOfNewsAsEnum = (Enums.NewsTypes)BaseNewsData.Type;
        }
    }
}
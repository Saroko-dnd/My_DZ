using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsInfrastructure;

namespace NewsWebsite.ClassesForNewsWebsite
{
    public class NewsForPartialView : News
    {
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

        public NewsForPartialView(News BaseNewsData)
        {
            Date = BaseNewsData.Date;
            Type = BaseNewsData.Type;
            Comments = BaseNewsData.Comments;
            HotNews = BaseNewsData.HotNews;
            NewsID = BaseNewsData.NewsID;
            Header = BaseNewsData.Header;
            Body = BaseNewsData.Body;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsDataAccess;
using NewsInfrastructure;

namespace NewsWebsite.Areas.News.Models
{
    public class SelectedNews
    {
        private NewsInfrastructure.News currentSelectedNews;
        private IEnumerable<Comment> commentsForSelectedNews;
        public NewsInfrastructure.News CurrentSelectedNews
        {
            get
            {
                return currentSelectedNews;
            }

            set
            {
                currentSelectedNews = value;
            }
        }

        public IEnumerable<Comment> CommentsForSelectedNews
        {
            get
            {
                return commentsForSelectedNews;
            }

            set
            {
                commentsForSelectedNews = value;
            }
        }
    }
}
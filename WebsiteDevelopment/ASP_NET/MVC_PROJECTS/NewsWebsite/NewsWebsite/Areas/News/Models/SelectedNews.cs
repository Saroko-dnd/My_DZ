using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsDataAccess;

namespace NewsWebsite.Areas.News.Models
{
    public class SelectedNews
    {
        private NewsDataAccess.News currentSelectedNews;
        private IEnumerable<Comment> commentsForSelectedNews;
        private long likes;
        private long dislikes;
        public NewsDataAccess.News CurrentSelectedNews
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

        public long Likes
        {
            get
            {
                return likes;
            }

            set
            {
                likes = value;
            }
        }

        public long Dislikes
        {
            get
            {
                return dislikes;
            }

            set
            {
                dislikes = value;
            }
        }
    }
}
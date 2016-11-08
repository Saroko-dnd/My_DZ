
using NewsInfrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewsDataAccess
{
    public class NewsWebsiteContext : DbContext, INewsWebsiteRepository
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserOpinion> LikesAndDislikes { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }

        public IEnumerable<Comment> AllComments
        {
            get
            {
                return Comments;
            }

            private set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<UserOpinion> AllLikesAndDislikes
        {
            get
            {
                return LikesAndDislikes;
            }

            private set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<News> AllNews
        {
            get
            {
                return News;
            }

            private set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<User> AllUsers
        {
            get
            {
                return Users;
            }

            private set
            {
                throw new NotImplementedException();
            }
        }

        public void AddNews(News NewNews)
        {
            News.Add(NewNews);
        }

        public void AddUser(User NewUser)
        {
            Users.Add(NewUser);
        }

        public void AddLikeOrDislike(UserOpinion NewUserOpinion)
        {
            LikesAndDislikes.Add(NewUserOpinion);
        }

        public void AddComment(Comment NewComment)
        {
            Comments.Add(NewComment);
        }

        public void SaveAllChangesMadeInsideCollections()
        {
            SaveChanges();
        }

        public NewsWebsiteContext(string ConnectionStringName): base(ConnectionStringName)
        {
            Database.SetInitializer(new NewsWebsiteDBInitializer());
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDataAccess
{
    public class NewsWebsiteDBInitializer : CreateDatabaseIfNotExists<NewsWebsiteContext>
    {
        protected override void Seed(NewsWebsiteContext Context)
        {
            List<News> TestListOfNews = new List<News>();
            List<User> TestListOfUsers = new List<User>();
            List<Comment> TestListOfComments = new List<Comment>();
            List<UserOpinion> TestListOfLikesAndDislikes = new List<UserOpinion>();

            for (int CounterOfUsers = 0; CounterOfUsers < 3; ++CounterOfUsers)
            {
                TestListOfUsers.Add(new User("Username_" + (CounterOfUsers + 1).ToString(), (CounterOfUsers + 1).ToString()));
            }

            for (int CounterOfNews = 0; CounterOfNews < 3; ++CounterOfNews)
            {
                if (CounterOfNews == 0)
                {
                    TestListOfNews.Add(new News(DateTime.Now, "Header_" + (CounterOfNews + 1).ToString(), "News body", false, 1));
                }
                else
                {
                    TestListOfNews.Add(new News(DateTime.Now, "Header_" + (CounterOfNews + 1).ToString(), "News body", true, 1));
                }
            }

            foreach (User CurrentUser in TestListOfUsers)
            {
                foreach (News CurrentNews in TestListOfNews)
                {
                    TestListOfComments.Add(new Comment(CurrentNews, CurrentUser,"This is comment"));
                }
            }

            int SecondCounterOfUsers;
            foreach (Comment CurrentComment in TestListOfComments)
            {
                SecondCounterOfUsers = 0;
                foreach (User CurrentUser in TestListOfUsers.Where(FoundUser => FoundUser != CurrentComment.Author).ToList())
                {
                    if ((SecondCounterOfUsers % 2) == 0)
                    {
                        TestListOfLikesAndDislikes.Add(new UserOpinion(CurrentComment, CurrentUser, true));
                    }
                    else
                    {
                        TestListOfLikesAndDislikes.Add(new UserOpinion(CurrentComment, CurrentUser, false));
                    }
                    ++SecondCounterOfUsers;
                }
            }

            foreach (News CurNews in TestListOfNews)
            {
                Context.News.Add(CurNews);
            }
            foreach (User CurUser in TestListOfUsers)
            {
                Context.Users.Add(CurUser);
            }
            foreach (Comment CurComment in TestListOfComments)
            {
                Context.Comments.Add(CurComment);
            }
            foreach (UserOpinion CurUserOpinion in TestListOfLikesAndDislikes)
            {
                Context.LikesAndDislikes.Add(CurUserOpinion);
            }

            base.Seed(Context);
        }
    }
}

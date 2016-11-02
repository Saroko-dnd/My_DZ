
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
            for (int CounterOfUsers = 0; CounterOfUsers < 3; ++CounterOfUsers)
            {
                Context.Users.Add(new User("Username_" + (CounterOfUsers + 1).ToString(), (CounterOfUsers + 1).ToString()));
            }

            for (int CounterOfNews = 0; CounterOfNews < 3; ++CounterOfNews)
            {
                if (CounterOfNews == 0)
                {
                    Context.News.Add(new News(DateTime.Now, "Header_" + (CounterOfNews + 1).ToString(), "News body", false, true));
                }
                else
                {
                    Context.News.Add(new News(DateTime.Now, "Header_" + (CounterOfNews + 1).ToString(), "News body", true, false));
                }
            }

            foreach (User CurrentUser in Context.Users)
            {
                foreach (News CurrentNews in Context.News)
                {
                    CurrentNews.Comments.Add(new Comment(CurrentUser, CurrentNews, "This is comment"));
                }
            }

            int SecondCounterOfUsers;
            foreach (Comment CurrentComment in Context.Comments)
            {
                SecondCounterOfUsers = 0;
                foreach (User CurrentUser in Context.Users.Where(FoundUser => FoundUser != CurrentComment.Author))
                {
                    if ((SecondCounterOfUsers % 2) == 0)
                    {
                        Context.LikesAndDislikes.Add(new UserOpinion(CurrentComment, CurrentUser, true));
                    }
                    else
                    {
                        Context.LikesAndDislikes.Add(new UserOpinion(CurrentComment, CurrentUser, false));
                    }
                    ++SecondCounterOfUsers;
                }
            }

            base.Seed(Context);
        }
    }
}

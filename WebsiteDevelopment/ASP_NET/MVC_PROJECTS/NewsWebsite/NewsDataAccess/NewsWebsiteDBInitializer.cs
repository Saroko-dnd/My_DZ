using NewsEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDataAccess
{
    public class NewsWebsiteDBInitializer : DropCreateDatabaseAlways<NewsWebsiteContext>
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
                    Context.News.Add(new News(DateTime.Now, "Header_" + (CounterOfNews + 1).ToString(), "News body", false));
                }
                else
                {
                    Context.News.Add(new News(DateTime.Now, "Header_" + (CounterOfNews + 1).ToString(), "News body", true));
                }
            }

            int CounterForCreatingOfComments = 0;
            foreach (User CurrentUser in Context.Users)
            {
                foreach (News CurrentNews in Context.News)
                {
                    Context.Comments.Add(new Comment(CurrentUser, CurrentNews, "This is comment"));
                    if (CounterForCreatingOfComments == 1)
                    {
                        Context.LikesAndDislikes.Add(new UserOpinion(CurrentNews, CurrentUser, false));
                    }
                    else
                    {
                        Context.LikesAndDislikes.Add(new UserOpinion(CurrentNews, CurrentUser, true));
                    }
                }
                ++CounterForCreatingOfComments;
            }

            base.Seed(Context);
        }
    }
}

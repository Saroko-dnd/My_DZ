using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsInfrastructure
{
    public interface INewsWebsiteRepository : IDisposable
    {
        IEnumerable<Comment> AllComments { get; }
        IEnumerable<UserOpinion> AllLikesAndDislikes { get; }
        IEnumerable<News> AllNews { get; }
        IEnumerable<User> AllUsers { get; }

        void AddNews(News NewNews);
        void AddUser(User NewUser);
        void AddLikeOrDislike(UserOpinion NewUserOpinion);
        void AddComment(Comment NewComment);

        void SaveAllChangesMadeInsideCollections();
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsInfrastructure
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CommentID { get; set; }
        public string Message{ get; set; }
        public DateTime Date{ get; set; }
        public long NewsID { get; set; }
        [ForeignKey("NewsID")]
        public News News { get; set; }
        public long? AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public User Author { get; set; }
        public ICollection<UserOpinion> LikesAndDislikes { get; set; }

        public Comment()
        {

        }

        public Comment(News CurrentNews, User CurrentUser, string CommentBody)
        {
            News = CurrentNews;
            Author = CurrentUser;
            Message = CommentBody;
            Date = DateTime.Now;
        }
    }
}

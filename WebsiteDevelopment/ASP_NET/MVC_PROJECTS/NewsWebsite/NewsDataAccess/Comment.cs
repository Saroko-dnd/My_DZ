using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntities
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint CommentID { get; set; }
        public string Message{ get; set; }
        public DateTime Date{ get; set; }
        public News News { get; set; }
        public User Author { get; set; }

        public Comment()
        {

        }

        public Comment(User CommentAuthor, News NewsToComment, string CommentBody)
        {
            Date = DateTime.Now;
            Message = CommentBody;
            News = NewsToComment;
            Author = CommentAuthor;
        }
    }
}

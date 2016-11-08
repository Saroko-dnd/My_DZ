
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsInfrastructure
{
    public class UserOpinion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserOpinionID { get; set; }
        public bool Like { get; set; }
        public long CommentID { get; set; }
        [ForeignKey("CommentID")]
        public Comment Comment { get; set; }
        public long? UserID { get; set; }
        [ForeignKey("UserID")]
        public User Author { get; set; }

        public UserOpinion()
        {

        }

        public UserOpinion(Comment CommentForLikeOrDislike, User CurrentUser, bool LikeIt)
        {
            Like = LikeIt;
            Comment = CommentForLikeOrDislike;
            Author = CurrentUser;
        }
    }
}

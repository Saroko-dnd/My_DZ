using NewsDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntities
{
    public class UserOpinion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint UserOpinionID { get; set; }
        public bool Like { get; set; }
        public News News { get; set; }
        public User Author { get; set; }

        public UserOpinion()
        {

        }

        public UserOpinion(News NewsForLikeOrDislike, User CurrentUser, bool LikeIt)
        {
            Like = LikeIt;
            News = NewsForLikeOrDislike;
            Author = CurrentUser;
        }
    }
}

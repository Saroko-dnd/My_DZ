using NewsEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsDataAccess
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint UserID{ get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserOpinion> LikesAndDislikes { get; set; }
    }
}

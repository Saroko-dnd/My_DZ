using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsEntities
{
    public class News
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint NewsID { get; set; }
        public string Header{ get; set; }
        public string Body{ get; set; }
        public DateTime Date{ get; set; }
        public bool HotNews { get; set; }
        public ICollection<UserOpinion> LikesAndDislikes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

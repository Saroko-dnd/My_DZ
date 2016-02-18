using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanksDBContext
{
    [Table("Comments")]
    public class Comment
    {
        [ForeignKey("RelatedBranch")]
        public int BranchID { get; set; }
        [Key]
        public int CommentID { get; set; }
        [Required]
        public string CommentItself { get; set; }
        [Required]
        public virtual BankBranch RelatedBranch { get; set; }
    }
}
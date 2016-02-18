using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace BanksDBContext
{
    [Table("BreakTimes")]
    public class BreakTime
    {
        [Key]
        public int BreakTimeID { get; set; }
        //public int BranchID { get; set; }
        [Required]
        public byte StartHour { get; set; }
        [Required]
        public byte StartMinutes { get; set; }
        [Required]
        public byte EndHour { get; set; }
        [Required]
        public byte EndMinutes { get; set; }
        public virtual ICollection<BankBranch> RelatedBranches { get; set; }
        public BreakTime()
        {
            RelatedBranches = new Collection<BankBranch>();
        }
    }
}

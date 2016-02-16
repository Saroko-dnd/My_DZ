using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanksOnMap.Entities
{
    [Table("BreakTimes")]
    public class BreakTime
    {
        [Key]
        public int BreakTimeID { get; set; }
        [Required]
        public byte StartHour { get; set; }
        [Required]
        public byte StartMinutes { get; set; }
        [Required]
        public byte EndHour { get; set; }
        [Required]
        public byte EndMinutes { get; set; }
        public virtual ICollection<BankBranch> RelatedBranches { get; set; }
    }
}

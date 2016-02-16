using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanksOnMap.Entities
{
    [Table("Banks")]
    public class Bank
    {
        [Key]
        public int BankID { get; set; }
        [Index(IsUnique = true)]
        public string BankName { get; set; }
        public virtual ICollection<BankBranch> RelatedBranches { get; set; }
    }
}

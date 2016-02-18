using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace BanksOnMap.Entities
{
    [Table("Services")]
    public class Service
    {
        [Key,Index(IsUnique = true)]
        public string Servise { get; set; }
        public virtual ICollection<BankBranch> RelatedBranches { get; set; }
        public Service()
        {
            RelatedBranches = new Collection<BankBranch>();
        }
    }
}

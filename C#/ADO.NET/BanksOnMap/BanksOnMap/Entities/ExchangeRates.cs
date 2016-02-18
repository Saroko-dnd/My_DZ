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
    [Table("ExchangeRates")]
    public class ExchangeRates
    {
        [Key]
        public int ExchangeRatesID { get; set; }
        [Required]
        public double USDSell { get; set; }
        [Required]
        public double USDBuy { get; set; }
        [Required]
        public double EUROSell { get; set; }
        [Required]
        public double EUROBuy { get; set; }
        [Required]
        public double RuSell { get; set; }
        [Required]
        public double RuBuy { get; set; }
        public virtual ICollection<BankBranch> RelatedBranches { get; set; }
        public ExchangeRates()
        {
            RelatedBranches = new Collection<BankBranch>();
        }
    }
}

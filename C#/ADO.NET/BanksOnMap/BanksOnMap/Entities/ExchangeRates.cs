using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public BankBranch RelatedBranch { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BanksOnMap.Entities
{
    [Table("BankBranches")]
    public class BankBranch
    {
        [Key]
        public int BankBranchID { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public Bank RelatedBank { get; set; }
        [Required]
        public ExchangeRates RelatedRates { get; set; }
        [Required]
        public DbGeography MapLocation { get ;set; }
        [Required]
        public DateTime OpeningDate { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Collections.ObjectModel;

namespace BanksDBContext
{
    [Table("BankBranches")]
    public class BankBranch
    {
        [Key]
        public int BankBranchID { get; set; }
        [ForeignKey("WorkingHours")]
        public int WorkHoursID { get; set; }
        [Required]
        public string BranchName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        [Required]
        public virtual Bank RelatedBank { get; set; }
        public virtual ExchangeRates RelatedRates { get; set; }
        public virtual Cashier RelatedCashier { get; set; }
        [Required]
        public DbGeography MapLocation { get ;set; }
        //необязательный DateTime ДОЛЖЕН быть Nullable
        public Nullable<DateTime> OpeningDate { get; set; }
        public virtual ICollection<Comment> RelatedComments { get; set; }
        public virtual ICollection<Service> RelatedServices { get; set; }
        public virtual WorkingHours WorkingHours { get; set; }
        public virtual ICollection<BreakTime> BreakTimes { get; set; }
        public BankBranch ()
        {
            RelatedComments = new Collection<Comment>();
            RelatedServices = new Collection<Service>();
            BreakTimes = new Collection<BreakTime>();
        }
    }
}

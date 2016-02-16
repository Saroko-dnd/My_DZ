using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanksOnMap.Entities
{
    [Table("Cashiers")]
    public class Cashier
    {
        [Key]
        public int CashierID { get; set; }
        [ForeignKey("RelatedBranch")]
        public int BranchID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public string Phone { get; set; }
        public BankBranch RelatedBranch { get; set; }
    }
}

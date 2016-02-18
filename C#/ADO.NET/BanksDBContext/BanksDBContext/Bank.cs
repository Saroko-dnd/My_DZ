
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
        [Table("Banks")]
        public class Bank
        {
            [Key]
            public int BankID { get; set; }
            //уникальность можно выставлять только для небольших строк (ограничение MS SQL Server)
            [Required, MaxLength(100), Index(IsUnique = true)]
            public string BankName { get; set; }
            public virtual ICollection<BankBranch> RelatedBranches { get; set; }

            public Bank()
            {
                RelatedBranches = new Collection<BankBranch>();
            }
        }
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityMigrationTest
{
    [Table("CarsSecondTable")]
    public class CarSecond
    {
        [Key]
        public int ID { get; set; }
        [Column("CarName")]
        public string Name { get; set; }
        public int YearOfCreation { get; set; }

    }
}

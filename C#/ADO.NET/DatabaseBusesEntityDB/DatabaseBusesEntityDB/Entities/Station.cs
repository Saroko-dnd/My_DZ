using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseBusesEntityDB
{
    [Table("Stations")]
    public class Station
    {
        [Key]
        public int StationID { get; set; }
        [Required]
        public string StationName { get; set; }
    }
}

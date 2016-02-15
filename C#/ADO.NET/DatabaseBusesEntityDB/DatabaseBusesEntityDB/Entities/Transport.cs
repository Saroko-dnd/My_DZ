using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseBusesEntityDB
{
    [Table("Transport")]
    public class Transport
    {
        [Key]
        public int TransportID { get; set; }
        [Required]
        public Route RouteForThisTransport { get; set; }
        [Required]
        public TypeOfVehicle CurrentType { get; set; }      
    }
}

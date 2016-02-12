using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseBusesEntityDB
{
    [Table("Routes")]
    public class Route
    {
        [Key]
        public int RouteID { get; set; }
        [Required]
        public string RouteName { get; set; }

        public virtual ICollection<Transport> Vehicles { get; set; }

        public virtual ICollection<Station> ListOfStations { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseBusesEntityDB
{
    [Table("VehicleTypes")]
    public class TypeOfVehicle
    {
        [Key]
        public int TypeID { get; set; }
        [Required]
        public string TypeName { get; set; }

        public virtual ICollection<Transport> Vehicles { get; set; }
    }
}

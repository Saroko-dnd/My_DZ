using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseBusesEntityDB
{
    [Table("Times")]
    public class Time
    {
        //public TimeSpan
        [Key]
        public int TimeID { get; set; }

        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:hh\\:mm}")]
        public DateTime TimeItself { get; set; }

        public virtual ICollection<Transport> ArrivedTransport { get; set; }

        public virtual ICollection<Station> RelatedStations { get; set; }
        
    }
}

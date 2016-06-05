using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug.DataBaseClasses
{
    [Table("Trains")]
    public class Train
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainID { get; set; }
        public string TrainName { get; set; }
        public int AmountOfFreeTickets { get; set; }

        public int? FromStationID { get; set; }
        public int? WhereStationID { get; set; }

        public virtual ICollection<Station> Stations { get; set; }

        public Train(string NewTrainName, int NewAmountOfFreeTickets)
        {
            TrainName = NewTrainName;
            AmountOfFreeTickets = NewAmountOfFreeTickets;
        }

        public Train()
        {

        }
    }
}

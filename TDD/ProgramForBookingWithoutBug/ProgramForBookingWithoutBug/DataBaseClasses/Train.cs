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
        public string TrainID { get; set; }
        public int AmountOfFreeTickets { get; set; }

        public Train(string NewTrainName, int NewAmountOfFreeTickets)
        {
            TrainID = NewTrainName;
            AmountOfFreeTickets = NewAmountOfFreeTickets;
        }

        public Train()
        {

        }
    }
}

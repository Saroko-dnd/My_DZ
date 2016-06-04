using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug.DataBaseClasses
{
    [Table("StationsAndTrains")]
    public class StationAndRelatedTrain
    {
        [Key, Column(Order = 0)]
        public string Station { get; set; }
        [Key, Column(Order = 1)]
        public string Train { get; set; }

        public StationAndRelatedTrain(string NewStationName, string NewTrainName)
        {
            Station = NewStationName;
            Train = NewTrainName;
        }

        public StationAndRelatedTrain()
        {

        }
    }
}

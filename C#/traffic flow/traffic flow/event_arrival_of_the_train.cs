using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class event_arrival_of_the_train : main_event
    {
        public int index_of_current_train;
        public int index_of_current_station;

        public override void processing_of_this_event()
        {
            Program.all_trains[index_of_current_train].arrival(index_of_current_station);
        }

        public event_arrival_of_the_train (int new_index_of_current_train, int new_index_of_current_station)
        {
            index_of_current_train = new_index_of_current_train;
            index_of_current_station = new_index_of_current_station;
        }
    }
}

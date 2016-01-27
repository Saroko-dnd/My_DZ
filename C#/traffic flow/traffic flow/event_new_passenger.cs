using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class event_new_passenger : main_event
    {
        public int index_of_current_station;
        public int index_of_target_station;
        public int line;

        public override void processing_of_this_event()
        {
            if (line == 1)
                Program.stations_first_side[index_of_current_station].add_passenger(index_of_target_station);
            else
                Program.stations_second_side[index_of_current_station].add_passenger(index_of_target_station);
        }

        public event_new_passenger(int new_current_station, int new_target_station, int new_line)
        {
            line = new_line;
            index_of_current_station = new_current_station;
            index_of_target_station = new_target_station;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class passenger
    {
        public int time_from_arrival = 0;
        public int index_of_target_station;
        public bool passenger_is_happy = true;
        public bool gone = false;

        public void activate_gone()
        {
            gone = true;
        }

        public void check_current_passenger()
        {
            ++time_from_arrival;
            if (time_from_arrival > Program.max_wait_time_for_passenger)
                passenger_is_happy = false;
        }
        public passenger (int new_target_station)
        {
            index_of_target_station = new_target_station;
        }
    }
}

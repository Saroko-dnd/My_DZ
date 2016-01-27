using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class station
    {
        public string name;
        public int median_time_to_next_station;
        public List<passenger> all_passengers = new List<passenger>();

        public int get_all_satisfied_passengers()
        {
            int buf_for_satisfied_passengers = 0;
            foreach (passenger current_passenger in all_passengers)
            {
                if (current_passenger.passenger_is_happy)
                    ++buf_for_satisfied_passengers;
            }
            return buf_for_satisfied_passengers;
        }

        public int get_all_dissatisfied_passengers()
        {
            int buf_for_dissatisfied_passengers = 0;
            foreach (passenger current_passenger in all_passengers)
            {
                if (!current_passenger.passenger_is_happy)
                {
                    ++buf_for_dissatisfied_passengers;
                }
            }
            return buf_for_dissatisfied_passengers;
        }
        public void time_passed()
        {
            foreach (passenger current_passenger in all_passengers)
            {
                current_passenger.check_current_passenger();
            }
        }

        public void add_passenger(int new_target_station)
        {
            all_passengers.Add(new passenger(new_target_station));
        }

        public void get_time (int new_time)
        {
            median_time_to_next_station = new_time;
        }

        public station (string new_name,int new_median_time_to_next_station)
        {
            name = new_name;
            median_time_to_next_station = new_median_time_to_next_station;
        }
    }
}

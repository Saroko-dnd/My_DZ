using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class train_struct_for_generating_of_arivals
    {
        public int index_of_current_train;
        public int index_of_station_for_arrival;
        public int time_before_arrival = 1;

        public void processing()
        {
            --time_before_arrival;
            if (time_before_arrival == 0)
            {
                Program.add_event_to_queue(new event_arrival_of_the_train
                    (index_of_current_train, index_of_station_for_arrival));
                if (index_of_station_for_arrival == 9)
                {
                    //тогда берем значение первой станции из другого массива
                    index_of_station_for_arrival = 0;
                    time_before_arrival = Program.main_random.Next(20,40);
                }
                else
                {
                    if (Program.all_trains[index_of_current_train].index_of_line == 1)
                    {
                        time_before_arrival = Program.stations_first_side[index_of_station_for_arrival].
                            median_time_to_next_station;
                    }
                    else
                    {
                        time_before_arrival = Program.stations_second_side[index_of_station_for_arrival].
                            median_time_to_next_station;
                    }
                    ++index_of_station_for_arrival;
                }
            }
        }

        public train_struct_for_generating_of_arivals(int new_index_of_current_train,
            int new_index_of_current_station)
        {
            index_of_current_train = new_index_of_current_train;
            index_of_station_for_arrival = new_index_of_current_station;
        }
    }
}

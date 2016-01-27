using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class all_transport_system
    {
        public List<station> all_stations_in_city_forward_direction;
        public List<station> all_stations_in_city_reverse_direction;
        public List<train> all_trains_in_city;

        public void transport_system_processing()
        {
            //проверка местонахождения поездов и генерация событий прибытия поездов
            for(int counter_2 = 0; counter_2 < all_stations_in_city.Count(); ++counter_2)
            {
                for (int counter = 0; counter < all_trains_in_city.Count(); ++counter)
                {
                    if (all_trains_in_city[counter].location_of_train == all_stations_in_city[counter_2].distance_point_of_station)
                    {
                        Program.add_event_to_queue(new event_arrival_of_the_train(counter, counter_2));
                    }
                }
            }

            foreach (train current_train in all_trains_in_city)
            {
                if (current_train.start_locaton_null)
                {
                    current_train.location_of_train += 1;
                    if (current_train.location_of_train == 
                        all_stations_in_city[all_stations_in_city.Count() - 1].distance_point_of_station)
                    {
                        current_train.start_locaton_null = false;
                    }
                }
                else
                {
                    current_train.location_of_train -= 1;
                    if (current_train.location_of_train == 0)
                    {
                        current_train.start_locaton_null = true;
                    }
                }
            }
        }
        //public void check_stations_events();
        //public void check_train_eents();
        //public void add_train(train new_train);

        public void handle_of_all_events(main_event new_event)
        {
            if (new_event.GetType() == typeof(event_new_passenger))
            {
                add_passenger((event_new_passenger)new_event);
            }
            else if (new_event.GetType() == typeof(event_arrival_of_the_train))
            {
                //add_passenger((event_new_passenger)new_event);
            }
        }

        public void add_passenger(event_new_passenger new_passenger)
        {
            all_stations_in_city[new_passenger.index_of_station].
                add_passenger(new passenger(new_passenger.index_of_next_station));
        }

        public all_transport_system (List<station> new_stations_forward,
            List<station> new_stations_reverse,List<train> new_trains)
        {
            all_stations_in_city_forward_direction = new_stations_forward;
            all_stations_in_city_reverse_direction = new_stations_reverse;
            all_trains_in_city = new_trains;
        }
    }
}

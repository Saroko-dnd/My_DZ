using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    class train
    {
        public string name;
        public int index_of_line;
        public int number_of_free_seats;
        public List<passenger> passengers_of_train = new List<passenger>();

        bool check_passenger(passenger current_passenger)
        {
            if (current_passenger.gone)
                return true;
            else
                return false;
        }

        public void arrival(int new_current_station)
        {
            if (index_of_line == 1)
            {
                for (int counter = 0; counter < passengers_of_train.Count(); ++counter)
                {
                    if (passengers_of_train[counter].index_of_target_station == new_current_station)
                    {
                        passengers_of_train[counter].activate_gone();
                        number_of_free_seats += 1;
                    }
                }
                passengers_of_train.RemoveAll(check_passenger);
                while (number_of_free_seats > 0 && Program.stations_first_side[new_current_station].
                    all_passengers.Count() > 0)
                {
                    --number_of_free_seats;
                    passengers_of_train.Add(Program.stations_first_side[new_current_station].all_passengers[0]);
                    Program.writer.WriteLine(Program.stations_first_side[new_current_station].
                        all_passengers[0].time_from_arrival);
                    Program.list_of_average_times.Add(Program.stations_first_side[new_current_station].
                        all_passengers[0].time_from_arrival);
                    Program.stations_first_side[new_current_station].
                        all_passengers.RemoveAt(0);
                }
                if (new_current_station == 9)
                    index_of_line = 2;
            }
            else
            {
                for (int counter = 0; counter < passengers_of_train.Count(); ++counter)
                {
                    if (passengers_of_train[counter].index_of_target_station == new_current_station)
                    {
                        passengers_of_train[counter].activate_gone();
                        number_of_free_seats += 1;
                    }
                }
                passengers_of_train.RemoveAll(check_passenger);
                while (number_of_free_seats > 0 && Program.stations_second_side[new_current_station].
                    all_passengers.Count() > 0)
                {
                    --number_of_free_seats;
                    passengers_of_train.Add(Program.stations_second_side[new_current_station].all_passengers[0]);
                    Program.writer.WriteLine(Program.stations_second_side[new_current_station].
                        all_passengers[0].time_from_arrival);
                    Program.list_of_average_times.Add(Program.stations_second_side[new_current_station].
                        all_passengers[0].time_from_arrival);
                    Program.stations_second_side[new_current_station].
                        all_passengers.RemoveAt(0);
                }
                if (new_current_station == 9)
                    index_of_line = 1;
            }
            Program.list_of_occupied_seats.Add(Program.number_of_seats_in_train - number_of_free_seats);
            Program.writer_2.WriteLine(Program.number_of_seats_in_train - number_of_free_seats);
        }

        public train(int new_index_of_line, int new_number_of_seats, string new_name)
        {
            name = new_name;
            index_of_line = new_index_of_line;
            number_of_free_seats = new_number_of_seats;
        }
    }
}

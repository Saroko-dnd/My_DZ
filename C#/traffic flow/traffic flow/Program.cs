using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace traffic_flow
{
    class Program
    {
        public static FileStream file = new FileStream("waiting_time.txt", FileMode.OpenOrCreate
            , FileAccess.Write);
        public static FileStream file_2 = new FileStream("occupancy_of_trains.txt", FileMode.OpenOrCreate
            , FileAccess.Write);
        public static FileStream file_3 = new FileStream("average_time.txt", FileMode.OpenOrCreate
            , FileAccess.Write);
        public static FileStream file_4 = new FileStream("median_time.txt", FileMode.OpenOrCreate
            , FileAccess.Write);
        public static FileStream file_5 = new FileStream("average_occupancy_in_percent.txt", FileMode.OpenOrCreate
            , FileAccess.Write);
        public static FileStream file_6 = new FileStream("median_occupancy_in_percent.txt", FileMode.OpenOrCreate
            , FileAccess.Write);
        public static StreamWriter writer = new StreamWriter(file);
        public static StreamWriter writer_2 = new StreamWriter(file_2);
        public static StreamWriter writer_3 = new StreamWriter(file_3);
        public static StreamWriter writer_4 = new StreamWriter(file_4);
        public static StreamWriter writer_5 = new StreamWriter(file_5);
        public static StreamWriter writer_6 = new StreamWriter(file_6);

        public const int max_wait_time_for_passenger = 800;
        public const int number_of_seats_in_train = 1000;

        public static int satisfied;
        public static int dissatisfied;

        public static List<int> list_of_waiting_times = new List<int>();
        public static List<int> list_of_average_times = new List<int>();
        public static List<int> list_of_occupied_seats = new List<int>();

        public static List<station> stations_first_side = new List<station>();
        public static List<station> stations_second_side = new List<station>();
        public static List<train> all_trains = new List<train>();
        public static List<train_struct_for_generating_of_arivals> list_of_trains_for_events = 
            new List<train_struct_for_generating_of_arivals>();

        public delegate void MainEventHandler(main_event current_event);
        static public event MainEventHandler event_was_created;
        public static Random main_random = new Random();
        //очередь событий
        public static List<main_event> list_of_all_events = new List<main_event>();
        static public void add_event_to_queue(main_event new_event)
        {
            list_of_all_events.Add(new_event);
        }

        static public void clear_queue()
        {
            list_of_all_events.Clear();
        }

        static public void generate_events()
        {
            int buf_int = main_random.Next(22);
            if (buf_int > 20)
            {
                for (int counter = buf_int - 20; counter > 0; --counter)
                {
                    int index_of_arrival_station = main_random.Next(9);
                    int index_of_next_station = main_random.Next(index_of_arrival_station,10);
                    if (main_random.Next(11) > 5)
                        add_event_to_queue(new event_new_passenger(index_of_arrival_station,
                            index_of_next_station,1));
                    else
                        add_event_to_queue(new event_new_passenger(index_of_arrival_station,
                            index_of_next_station, 2));
                }
            }

            foreach (train_struct_for_generating_of_arivals current_struct in list_of_trains_for_events)
            {
                current_struct.processing();
            }
        }

        static public void handle_of_all_events(main_event new_event)
        {
            new_event.processing_of_this_event();
        }

        static public void ratio_satisfied_dissatisfied()
        {
            int global_buf_for_satisfied = 0;
            int global_buf_for_dissatisfied = 0;
            foreach (station current_station in stations_first_side)
            {
                global_buf_for_satisfied += current_station.get_all_satisfied_passengers();
            }
            foreach (station current_station in stations_second_side)
            {
                global_buf_for_satisfied += current_station.get_all_satisfied_passengers();
            }

            foreach (station current_station in stations_first_side)
            {
                global_buf_for_dissatisfied += current_station.get_all_dissatisfied_passengers();
            }
            foreach (station current_station in stations_second_side)
            {
                global_buf_for_dissatisfied += current_station.get_all_dissatisfied_passengers();
            }
            satisfied = global_buf_for_satisfied;
            dissatisfied = global_buf_for_dissatisfied;
        }

        static void Main(string[] args)
        {
            //создание транспортной системы******************************************************
            //первая цепочка остановок
            for (int counter = 1; counter < 11; ++counter)
            {
                string buf_string = "station_first_side_" + counter.ToString();
                if ((counter + 1) == 11)
                    stations_first_side.Add(new station(buf_string, 0));
                else
                     stations_first_side.Add(new station(buf_string, counter * main_random.Next(1, 100)));
            }

            //обратная цепочка остановок
            for (int counter = 1; counter < 11; ++counter)
            {
                string buf_string = "station_first_side_" + counter.ToString();
                if (counter < 10)
                    stations_second_side.Add(new station(buf_string, 
                        stations_first_side[9 - counter].median_time_to_next_station));
                else
                    stations_second_side.Add(new station(buf_string,0));
            }

            all_trains.Add(new train(1, number_of_seats_in_train, "train_first"));
            all_trains.Add(new train(2, number_of_seats_in_train, "train_second"));
            list_of_trains_for_events.Add(new train_struct_for_generating_of_arivals
                (0, 0));
            list_of_trains_for_events.Add(new train_struct_for_generating_of_arivals
                (1, 0));
            //транспортная система создана***********************************************************

            //добавляем реакции на событие
            event_was_created += handle_of_all_events;

            int number_of_cycles = 500005;
            //главный цикл
            for (int counter = 0; counter < number_of_cycles; ++counter)
            {
                //генерация событий
                generate_events();
                //обработка всех событий в очереди
                foreach (main_event current_event in list_of_all_events)
                {
                    event_was_created(current_event);
                }
                //очистка очереди событий
                clear_queue();

                //увеличение времени ожидания у всеж людей на остоновках
                foreach (station current_station in stations_first_side)
                {
                    current_station.time_passed();
                }
                foreach (station current_station in stations_second_side)
                {
                    current_station.time_passed();
                }
                //получаем количество довольных и недовольных пассажиров
                ratio_satisfied_dissatisfied();
                //проверяем надо ли добавить или убрать поезд
                if ((counter%2000 == 0) && (counter > 0))
                {
                    Console.WriteLine("количество довольных пассажиров: " + satisfied.ToString());
                    Console.WriteLine("количество недовольных пассажиров: " + dissatisfied.ToString());
                    if (satisfied < dissatisfied)
                    {
                        int number_of_free_seats = 0;
                        int max_number_of_free_seats = (number_of_seats_in_train / 2) * all_trains.Count();
                        for (int counter_2 = 0; counter_2 < all_trains.Count(); ++counter_2)
                        {
                            number_of_free_seats += all_trains[counter_2].number_of_free_seats;
                        }
                        if (number_of_free_seats < max_number_of_free_seats)
                        {
                            train buf_train = new train(main_random.Next(1, 3), number_of_seats_in_train,
                                "train_" + counter.ToString());
                            all_trains.Add(buf_train);
                            list_of_trains_for_events.Add(new train_struct_for_generating_of_arivals((all_trains.
                                Count() - 1), 0));
                            Console.WriteLine("ДОБАВЛЕН НОВЫЙ ПОЕЗД");
                        }
                    }
                    else
                    {
                        if (all_trains.Count() > 1)
                        {
                            int number_of_free_seats = 0;
                            int max_number_of_free_seats = (number_of_seats_in_train / 2) * all_trains.Count();
                            for (int counter_2 = 0; counter_2 < all_trains.Count(); ++counter_2)
                            {
                                number_of_free_seats += all_trains[counter_2].number_of_free_seats;
                            }
                            if (number_of_free_seats > max_number_of_free_seats)
                            {
                                all_trains.RemoveAt(all_trains.Count() - 1);
                                list_of_trains_for_events.RemoveAt(list_of_trains_for_events.Count() - 1);
                                Console.WriteLine("УДАЛЕН ПОЕЗД (слишком много пустых мест)");
                            }
                        }
                    }
                    Console.WriteLine("текущее количество поездов: " + all_trains.Count().ToString());
                    foreach (train current_train in all_trains)
                    {
                        Console.WriteLine(current_train.name + " пустых мест " + current_train.number_of_free_seats);
                    }
                    Console.WriteLine("********************************************************");

                    int buf_for_median = 0;
                    //сбор статистики (начало)************************************************************
                    writer_3.WriteLine((int)list_of_average_times.Average());
                    buf_for_median = list_of_average_times.Median();
                    if (buf_for_median >= 0)
                        writer_4.WriteLine(buf_for_median);
                    writer_5.WriteLine((int)list_of_occupied_seats.Average() / 10);
                    buf_for_median = list_of_occupied_seats.Median();
                    if (buf_for_median >= 0)
                        writer_6.WriteLine((int)buf_for_median / 10);
                    list_of_average_times.Clear();
                    list_of_occupied_seats.Clear();
                    //(конец)***************************************************************************
                }

            }

            writer.Close();
            file.Close();
            writer_2.Close();
            file_2.Close();
            writer_3.Close();
            file_3.Close();
            writer_4.Close();
            file_4.Close();
            writer_5.Close();
            file_5.Close();
            writer_6.Close();
            file_6.Close();

            Console.ReadKey();
        }
    }
}

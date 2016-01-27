using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace first_trying
{
    class Program
    {
        public static void Main(string[] args)
        {
            var my_var = Math.Atan(1) * 4;//аналог AUTO из С++
            int[] array = new int[5]{1,2,3,4,5};//массив
            int[][] matrix = new int [5][];//массив из массивов
            int[,] matrix_2 = new int[2, 2];//массив размером 2 на 2 (гарантированно)
            for (int counter = 0; counter < matrix.Length; ++counter)
            {
                matrix[counter] = new int[2];
            }
            array[3] = 4;
            for (int counter = 0; counter < array.Length;++counter )
            {
                Console.WriteLine(array[counter]);
            }
                Console.WriteLine("hi to all");
                vector vec_1 = new vector(2, 3, 4);
                vector vec_2 = new vector(5, 6, 7);
                vector vec_3 = new vector(0, 0, 0);
                vec_3 = vector.multiplication(vec_1, vec_2);
                Console.WriteLine("reult of multiplication");
                Console.WriteLine(vec_3.our_vector[0]);
                Console.WriteLine(vec_3.our_vector[1]);
                Console.WriteLine(vec_3.our_vector[2]);
                bird our_bird = new bird("bird_");
                cat our_cat = new cat("ktar");
                our_cat.voice();
                our_bird.voice();

                int[] our_array = new int[10]{8,6,7,6,5,7,45,3,2,13};
                sort_array_class<int> our_class_array_sort = new sort_array_class<int>(our_array);
                our_class_array_sort.start_quicksort();
                Console.WriteLine("Sorted array (quick sort)");
                foreach (int cur_int in our_class_array_sort.current_array)
                {
                    Console.WriteLine(cur_int);
                }

                List<animal> list_of_animals = new List<animal>();
                list_of_animals.Add(new bird("bird_1"));
                list_of_animals.Add(new cat("cat_1"));
                list_of_animals.Add(new bird("bird_2"));
                list_of_animals.Add(new cat("cat_2"));
                list_of_animals.Add(new bird("bird_3"));
                list_of_animals.Add(new cat("cat_3"));
                list_of_animals.Add(new bird("bird_4"));
                list_of_animals.Add(new cat("cat_4"));
                list_of_animals.Add(new bird("bird_5"));
                list_of_animals.Add(new cat("cat_5"));
                IFlyers flyers_check;
            foreach (animal cur_animal in list_of_animals)
            {
                try
                {
                    flyers_check = (IFlyers)cur_animal;
                    flyers_check.to_fly();

                }
                catch(InvalidCastException cur_exeption)
                {
                    Console.WriteLine("this cat cant fly");
                }
                
            }

            //LISTS OF VIDEOS............................................................
                int index_position_of_max_payed_time = 0;
                int number_of_screens = 2;
                int current_video = -1;
                int buf = -1;
                float max_payed_time = -1.0f;
                List<List<video_info>> list_of_screens = new List<List<video_info>>();
            for (int counter = 0; counter < number_of_screens; ++counter)
            {
                list_of_screens.Add(new List<video_info>());
            }
                List<video_info> list_of_videos = new List<video_info>();
                list_of_videos.Add(new video_info ("video_1", 30.0f, 10800.0f));
                list_of_videos.Add(new video_info("video_2", 15.0f, 3600.0f));
                list_of_videos.Add(new video_info("video_3", 45.0f, 7200.0f));
                list_of_videos.Add(new video_info("video_4", 60.0f, 4800.0f));
                list_of_videos.Add(new video_info("video_5", 20.0f, 6600.0f));
                Random our_random_generator = new Random();
                float sum_of_payed_time = 0.0f;
                foreach (video_info current_video_info in list_of_videos)
                {
                    sum_of_payed_time += current_video_info.payed_time;
                }
                int counter_ = 0;
                foreach (video_info current_video_info in list_of_videos)
                {
                    if (current_video_info.payed_time > max_payed_time)
                    {
                        max_payed_time = current_video_info.payed_time;
                        index_position_of_max_payed_time = counter_;
                    }
                    ++counter_;
                }
                int sum_of_payed_time_int = (int)sum_of_payed_time;
                int min_of_downtime = list_of_videos.Count() + 1;
                for (int counter = 0; counter < (min_of_downtime - 1); ++counter )
                {
                    list_of_videos[counter].set_max_downtime((int)(min_of_downtime * (max_payed_time / list_of_videos[counter].payed_time)));
                }

                int elapsed_time = 0, finding_video_buf = 0,finding_video_counter = 0;
                bool was_max_downtime = false,previous_was_different = false;
                while (elapsed_time < sum_of_payed_time_int)
                {
                    for (int counter = 0; counter < number_of_screens; ++counter)
                    {
                        foreach (video_info current_video_info in list_of_videos)
                        {
                            if (current_video_info.downtime == current_video_info.max_downtime &&
                                !was_max_downtime)
                            {
                                list_of_screens[counter].Add(current_video_info);
                                current_video_info.mark_as_shown();
                                was_max_downtime = true;
                                elapsed_time += (int)current_video_info.time;
                                current_video_info.downtime_to_zero();
                            }
                        }
                        if (!was_max_downtime)
                        {
                            while (!previous_was_different)
                            {
                                current_video = our_random_generator.Next(sum_of_payed_time_int);
                                while (finding_video_buf < current_video)
                                {
                                    finding_video_buf += (int)list_of_videos[finding_video_counter].payed_time;
                                    ++finding_video_counter;
                                }
                                if ((list_of_screens[counter].Count() > 0) && (list_of_screens[counter][list_of_screens[counter].Count() - 1].name ==  
                                list_of_videos[finding_video_counter - 1].name))
                                {
                                    finding_video_buf = 0;
                                    finding_video_counter = 0;
                                }
                                else
                                {
                                    previous_was_different = true;
                                }
                            }
                            previous_was_different = false;
                            list_of_screens[counter].Add(list_of_videos[finding_video_counter - 1]);
                            list_of_videos[finding_video_counter - 1].mark_as_shown();
                            elapsed_time += (int)list_of_videos[finding_video_counter - 1].time;
                            finding_video_buf = 0;
                            finding_video_counter = 0;
                        }
                        else
                        {
                            was_max_downtime = false;
                        }
                    }
                    foreach (video_info current_video_info in list_of_videos)
                    {
                        if (!current_video_info.was_shown)
                        {
                            current_video_info.increment_downtime();
                        }
                        else
                        {
                            current_video_info.shown_to_false();
                            current_video_info.downtime_to_zero();
                        }
                    }

                }
                int number_of_file = 1;
            //print lists for screens
            for (int counter = 0; counter <number_of_screens; ++counter)
            {
                String result = "list_screen_" + number_of_file.ToString() + ".txt";
                ++number_of_file;
                FileStream file = new FileStream(result, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter write_stream = new StreamWriter(file);
                for (int counter_2 = 0; counter_2 < list_of_screens[0].Count(); ++counter_2)
                    write_stream.WriteLine(list_of_screens[counter][counter_2].name);
                write_stream.Close();
                file.Close();
            }
            elapsed_time = 0;
            for (int counter = 0; counter < list_of_videos.Count(); ++counter)
            {
                for (int counter_2 = 0; counter_2 < number_of_screens; ++counter_2)
                {
                    String result = list_of_videos[counter].name + "_screen_" + (counter_2+1).ToString() + ".txt";
                    FileStream file = new FileStream(result, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter write_stream = new StreamWriter(file);
                    for (int counter_3 = 0; counter_3 < list_of_screens[counter_2].Count(); ++counter_3)
                    {
                        if (list_of_screens[counter_2][counter_3].name == list_of_videos[counter].name)
                        {
                            String result_2 = list_of_screens[counter_2][counter_3].name + " time " +
                                elapsed_time.ToString();
                            write_stream.WriteLine(result_2);
                        }
                        elapsed_time += (int)list_of_screens[counter_2][counter_3].time;
                    }
                    elapsed_time = 0;
                    write_stream.Close();
                    file.Close();
                }
            }
       
        }
    }

    class video_info
    {
        public string name;
        public float time;
        public float payed_time;
        public int downtime;
        public int max_downtime;
        public bool was_shown;
        public void set_max_downtime(int number_to_set)
        {
            max_downtime = number_to_set;
        }
        public void shown_to_false()
        {
            was_shown = false;
        }
        public void mark_as_shown()
        {
            was_shown = true;
        }
        public void downtime_to_zero()
        {
            downtime = 0;
        }
        public void increment_downtime()
        {
            downtime += 1;
        }
        public video_info(string new_name, float new_time, float new_payed_time)
        {
            name = new_name;
            time = new_time;
            payed_time = new_payed_time;
            downtime = 0;
            was_shown = false;
            max_downtime = 0;
        }
    }

    class sort_array_class<T> where T : IComparable
    {

        public T[] current_array;
        public void buble_sort()
        {
            T bufer = current_array[0];
            for (int counter = 0; counter < current_array.Length; ++counter)
            {
                for (int counter_2 = counter + 1; counter_2 < current_array.Length; ++counter_2)
                {
                    if (current_array[counter].CompareTo(current_array[counter_2])  > 0)
                    {
                        bufer = current_array[counter_2];
                        current_array[counter_2] = current_array[counter];
                        current_array[counter] = bufer;
                    }
                }
            }
        }
        public int partition(T[] array, int start, int end)
        {
            int marker = start;
            for (int i = start; i <= end; i++)
            {
                if (array[i].CompareTo(array[end]) <= 0)
                {
                    T temp = array[marker]; 
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                }
            }
            return marker - 1;
        }
        public void start_quicksort()
        {
            quicksort(current_array, 0, current_array.Length - 1);
        }
        public void quicksort(T[] array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = partition(array, start, end);
            quicksort(array, start, pivot - 1);
            quicksort(array, pivot + 1, end);
        }

        public sort_array_class(T[] new_array)
        {
            current_array = new_array;
        }
    }
}

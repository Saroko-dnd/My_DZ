using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace rating_system
{

    public class operation
    {
        public string operation_name;
        public string operation_description;

        public string OperationName
        {
            get
            {
                return operation_name;
            }
            set
            {
                operation_name = value;
            }
        }

        public string OperationDescription
        {
            get
            {
                return operation_description;
            }
            set
            {
                operation_description = value;
            }
        }

        public operation(string new_operation, string new_description)
        {
            operation_name = new_operation;
            operation_description = new_description;
        }
    }

    public class year_rating : INotifyPropertyChanged
    {
        public int rating = 0;
        public string year;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }

        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
                OnPropertyChanged("Rating_string");
            }
        }

        public string Rating_string
        {
            get
            {
                return rating.ToString();
            }
            set
            {
                rating = Int32.Parse(value);
                OnPropertyChanged("Rating");
                OnPropertyChanged("Rating_string");
            }
        }

        public year_rating (string new_year, int new_rating)
        {
            rating = new_rating;
            year = new_year;
        }

    }

    public class photo
    {
        public BitmapImage picture;
        public BitmapImage Photo
        {
            get
            {
                return picture;
            }
            set
            {
                picture = value;
            }
        }

        public photo (BitmapImage new_face)
        {
            picture = new_face;
        }
    }

    public class great_assassin : INotifyPropertyChanged
    {
        //new BitmapImage(new Uri(path_to_picture, UriKind.Relative));
        BitmapImage picture;
        string first_name;
        string second_name;
        int fame = 0;
        int battle_against_many_guards = -1;
        int stealth_orders = -1;
        int work_in_team = -1;
        int completed_orders_last_year = 0;
        /// <summary>
        /// Количество жертв убитых при помощи ловушек
        /// </summary>
        int kill_target_with_trap = -1;
        /// <summary>
        /// Количество жертв убитых при помощи яда
        /// </summary>
        int kill_target_with_poison = -1;

        int current_id;
        string home_location;
        string work_status;
        int salary;
        string education;


        LinearGradientBrush current_brush = new LinearGradientBrush() { ColorInterpolationMode = System.Windows.Media.ColorInterpolationMode.SRgbLinearInterpolation,
                        StartPoint = new Point(0,0),
                        EndPoint = new Point(0,1)};

        GradientStopCollection current_collection_for_gradient;

        ObservableCollection<KeyValuePair<string, int>> ratings_this_year = new ObservableCollection<KeyValuePair<string, int>>();
        ObservableCollection<operation> list_of_operations;
        //List<operation> list_of_operations;
        ObservableCollection<year_rating> ratings_last_5_years = new ObservableCollection<year_rating>();
        ObservableCollection<KeyValuePair<string, int>> ratings_last_5_years_2 = new ObservableCollection<KeyValuePair<string, int>>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<KeyValuePair<string, int>> RatingsThisYear
        {
            get
            {
                return ratings_this_year;
            }
            set
            {
                if (value != ratings_this_year)
                {
                    ratings_this_year = value;
                    OnPropertyChanged("RatingsThisYear");
                }
            }
        }

        public ObservableCollection<operation> Operations
        {
            get
            {
                return list_of_operations;
            }
            set
            {
                if (value != list_of_operations)
                {
                    list_of_operations = value;
                    OnPropertyChanged("Operations");
                }
            }
        }

        public string assassinId
        {
            get
            {
                return current_id.ToString();
            }
            set
            {
                    current_id = Int32.Parse(value);
                    OnPropertyChanged("assassinId");
            }
        }

        public string Location
        {
            get
            {
                return home_location;
            }
            set
            {
                    home_location = value;
                    OnPropertyChanged("Location");
            }
        }

        public string Rank
        {
            get
            {
                return work_status;
            }
            set
            {
                    work_status = value;
                    OnPropertyChanged("Rank");
            }
        }

        public string Salary
        {
            get
            {
                return salary.ToString();
            }
            set
            {
                    salary = Int32.Parse(value);
                    OnPropertyChanged("Salary");
            }
        }

        public string Education
        {
            get
            {
                return education;
            }
            set
            {
                    education = value;
                    OnPropertyChanged("Education");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<KeyValuePair<string, int>> RatingsLast5Years_keyValue
        {
            get
            {
                return ratings_last_5_years_2;
            }
            set
            {
                if (value != ratings_last_5_years_2)
                {
                    ratings_last_5_years_2 = value;
                    OnPropertyChanged("RatingsLast5Years_keyValue");
                }
            }
        }

        public ObservableCollection<year_rating> RatingsLast5Years
        {
            get
            {
                return ratings_last_5_years;
            }
            set
            {
                    ratings_last_5_years = value;
                    ObservableCollection<KeyValuePair<string, int>> buf_for_ratings_last_5_years = new ObservableCollection<KeyValuePair<string, int>>();
                    for (int counter = 0; counter < 5; ++ counter)
                    {
                        buf_for_ratings_last_5_years.Add(new KeyValuePair<string, int>(ratings_last_5_years[counter].
                            Year, ratings_last_5_years[counter].Rating));
                    }
                    ratings_last_5_years_2 = buf_for_ratings_last_5_years;
                    OnPropertyChanged("RatingsLast5Years_keyValue");
                    OnPropertyChanged("RatingsLast5Years");
            }
        }

        public BitmapImage Photo
        {
            get
            {
                return picture;
            }
            set
            {
                if (value != picture)
                {
                    picture = value;
                    OnPropertyChanged("Photo");
                }
            }
        }

        public string FirstName
        {
            get
            {
                return first_name;
            }
            set
            {
                if (value != first_name)
                {
                    first_name = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        public string SecondName
        {
            get
            {
                return second_name;
            }
            set
            {
                if (value != second_name)
                {
                    second_name = value;
                    OnPropertyChanged("SecondName");
                }
            }
        }

        public string Fame_string
        {
            get
            {
                return fame.ToString();
            }
            set
            {
                if (Int32.Parse(value) != fame)
                {
                    fame = Int32.Parse(value);
                    OnPropertyChanged("Fame");
                }
            }
        }

        public int Fame
        {
            get
            {
                return fame;
            }
            set
            {
                fame = value;
                OnPropertyChanged("Fame");
            }
        }

        public LinearGradientBrush GradientBrush
        {
            get
            {
                return current_brush;
            }
            set
            {
                current_brush = value;
                OnPropertyChanged("GradientBrush");
            }
        }

        public string DefeatedManyGuards_string
        {
            get
            {
                if (battle_against_many_guards >= 0)
                    return battle_against_many_guards.ToString();
                else
                    return "";
            }
            set
            {
                try
                {
                    if (Int32.Parse(value) != battle_against_many_guards)
                    {
                        fame -= battle_against_many_guards;
                        fame += Int32.Parse(value);
                        battle_against_many_guards = Int32.Parse(value);
                        ratings_this_year[0] = new KeyValuePair<string, int>(my_resourses.texts.kill_many_guards, Int32.Parse(value));
                    }
                }
                catch
                {
                    ratings_this_year[0] = new KeyValuePair<string, int>(my_resourses.texts.kill_many_guards, 0);
                    fame -= battle_against_many_guards;
                    battle_against_many_guards = 0;
                }
                update_diagrams_data();
                update_gradient_brush();
                OnPropertyChanged("RatingsLast5Years_keyValue");
                OnPropertyChanged("RatingsLast5Years");
                OnPropertyChanged("Fame_string");
                OnPropertyChanged("DefeatedManyGuards");
                OnPropertyChanged("ratings_this_year");
            }
        }

        public int DefeatedManyGuards
        {
            get
            {
                return battle_against_many_guards;
            }
            set
            {
                fame += value;
                battle_against_many_guards = value;
                update_gradient_brush();
                ratings_this_year[0] = new KeyValuePair<string, int>(my_resourses.texts.kill_many_guards, value);
            }
        }

        public string StealthOrders_string
        {
            get
            {
                if (stealth_orders >= 0)
                    return stealth_orders.ToString();
                else
                    return "";
            }
            set
            {
                try
                {
                    if (Int32.Parse(value) != stealth_orders)
                    {
                        fame -= stealth_orders;
                        fame += Int32.Parse(value);
                        stealth_orders = Int32.Parse(value);
                        ratings_this_year[1] = new KeyValuePair<string, int>(my_resourses.texts.stealth_orders, Int32.Parse(value));
                    }
                }
                catch
                {
                    fame -= stealth_orders;
                    stealth_orders = 0;
                    ratings_this_year[1] = new KeyValuePair<string, int>(my_resourses.texts.stealth_orders, 0);
                }
                update_gradient_brush();
                update_diagrams_data();
                OnPropertyChanged("RatingsLast5Years_keyValue");
                OnPropertyChanged("RatingsLast5Years");
                OnPropertyChanged("Fame_string");
                OnPropertyChanged("StealthOrders");
                OnPropertyChanged("ratings_this_year");
            }
        }

        public int StealthOrders
        {
            get
            {
                return stealth_orders;
            }
            set
            {
                fame += value;
                stealth_orders = value;
                update_gradient_brush();
                ratings_this_year[1] = new KeyValuePair<string, int>(my_resourses.texts.stealth_orders, value);
            }
        }

        public string TeamWork_string
        {
            get
            {
                if (work_in_team >= 0)
                    return work_in_team.ToString();
                else
                    return "";
            }
            set
            {
                try
                {
                    if (Int32.Parse(value) != work_in_team)
                    {
                        fame -= work_in_team;
                        fame += Int32.Parse(value);
                        work_in_team = Int32.Parse(value);
                        ratings_this_year[2] = new KeyValuePair<string, int>(my_resourses.texts.work_in_team, Int32.Parse(value));
                    }
                }
                catch
                {
                    fame -= work_in_team;
                    work_in_team = 0;
                    ratings_this_year[2] = new KeyValuePair<string, int>(my_resourses.texts.work_in_team, 0);
                }
                update_gradient_brush();
                update_diagrams_data();
                OnPropertyChanged("RatingsLast5Years_keyValue");
                OnPropertyChanged("RatingsLast5Years");
                OnPropertyChanged("Fame_string");
                OnPropertyChanged("TeamWork");
                OnPropertyChanged("ratings_this_year");
            }
        }

        public int TeamWork
        {
            get
            {
                return work_in_team;
            }
            set
            {
                fame += value;
                work_in_team = value;
                update_gradient_brush();
                ratings_this_year[2] = new KeyValuePair<string, int>(my_resourses.texts.work_in_team, value);
            }
        }

        public int CompletedOrdersLastYear
        {
            get
            {
                return completed_orders_last_year;
            }
            set
            {
                completed_orders_last_year = value;
            }
        }

        public string KilledWithTraps_string
        {
            get
            {
                if (kill_target_with_trap >= 0)
                    return kill_target_with_trap.ToString();
                else
                    return "";
            }
            set
            {
                try
                {
                    if (Int32.Parse(value) != kill_target_with_trap)
                    {
                        fame -= kill_target_with_trap;
                        fame += Int32.Parse(value);
                        kill_target_with_trap = Int32.Parse(value);
                        ratings_this_year[3] = new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_trap, Int32.Parse(value));
                    }
                }
                catch
                {
                    fame -= kill_target_with_trap;
                    kill_target_with_trap = 0;
                    ratings_this_year[3] = new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_trap, 0);
                }
                update_gradient_brush();
                update_diagrams_data();
                OnPropertyChanged("RatingsLast5Years_keyValue");
                OnPropertyChanged("RatingsLast5Years");
                OnPropertyChanged("Fame_string");
                OnPropertyChanged("KilledWithTraps");
                OnPropertyChanged("ratings_this_year");
            }
        }

        public int KilledWithTraps
        {
            get
            {
                return kill_target_with_trap;
            }
            set
            {
                fame += value;
                kill_target_with_trap = value;
                update_gradient_brush();
                ratings_this_year[3] = new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_trap, value);
            }
        }

        public string KilledWithPoison_string
        {
            get
            {
                if (kill_target_with_poison >= 0)
                    return kill_target_with_poison.ToString();
                else
                    return "";
            }
            set
            {
                try
                {
                    if (Int32.Parse(value) != kill_target_with_poison)
                    {
                        fame -= kill_target_with_poison;
                        fame += Int32.Parse(value);
                        kill_target_with_poison = Int32.Parse(value);
                        ratings_this_year[4] = new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_poison, Int32.Parse(value));
                    }
                }
                catch
                {
                    fame -= kill_target_with_poison;
                    kill_target_with_poison = 0;
                    ratings_this_year[4] = new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_poison, 0);
                }
                update_gradient_brush();
                update_diagrams_data();
                OnPropertyChanged("RatingsLast5Years_keyValue");
                OnPropertyChanged("RatingsLast5Years");
                OnPropertyChanged("Fame_string");
                OnPropertyChanged("KilledWithPoison");
                OnPropertyChanged("ratings_this_year");
            }
        }



        public int KilledWithPoison
        {
            get
            {
                return kill_target_with_poison;
            }
            set
            {
                fame += value;
                kill_target_with_poison = value;
                update_gradient_brush();
                ratings_this_year[4] = new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_poison, value);
            }
        }

        private void update_diagrams_data()
        {
            ratings_last_5_years.RemoveAt(ratings_last_5_years.Count - 1);
            ratings_last_5_years.Add(new year_rating("2015", fame));
            ratings_last_5_years_2.RemoveAt(ratings_last_5_years_2.Count - 1);
            ratings_last_5_years_2.Add(new KeyValuePair<string, int>("2015", fame));
        }

        private void update_gradient_brush()
        {
            current_brush.GradientStops.Clear();
            if (fame >= 2000)
            {
                current_brush.GradientStops.Add(new GradientStop() { Color = Colors.LightGreen, Offset = 0 });
                current_brush.GradientStops.Add(new GradientStop() { Color = Colors.DarkGreen, Offset = 1 });
            }
            else if (fame >= 1000)
            {
                current_brush.GradientStops.Add(new GradientStop() { Color = Colors.LightGoldenrodYellow, Offset = 0 });
                current_brush.GradientStops.Add(new GradientStop() { Color = Colors.Orange, Offset = 1 });
            }
            else
            {
                current_brush.GradientStops.Add(new GradientStop() { Color = Colors.OrangeRed, Offset = 0 });
                current_brush.GradientStops.Add(new GradientStop() { Color = Colors.DarkRed, Offset = 1 });
            }
            OnPropertyChanged("GradientBrush");
        }

        public great_assassin(string path_to_picture, 
            string new_first_name, string new_second_name)
        {
            picture = new BitmapImage(new Uri(path_to_picture, UriKind.Relative));
            first_name = new_first_name;
            second_name = new_second_name;
            for (int counter = 0; counter < 5; ++ counter )
            {
                ratings_this_year.Add(new KeyValuePair<string, int>(string.Empty, 0));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace rating_system
{


    class list_box_operations_height_converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((double)value) - 0.3 * ((double)value)) - 300.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class list_box_operations_converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (value as great_assassin).Operations;
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class list_box_item_to_items_control : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (value as great_assassin).RatingsLast5Years;
            else
            {
                ObservableCollection<year_rating> new_data_source = new ObservableCollection<year_rating>();
                int buf_for_year = 2011;
                for (int counter = 0; counter < 5; ++counter)
                {
                    new_data_source.Add(new year_rating(buf_for_year.ToString(), 0));
                    ++buf_for_year;
                }
                return new_data_source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class list_box_item_to_ratings_data : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return value as great_assassin;
            else
                return new great_assassin(@"/my_resourses/portraits/face_1.jpg",
                " "," ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class string_converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "";
            else
                return value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value as string;
        }
    }

    class list_box_height_converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value) - 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class list_box_item_to_diagram : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return (value as great_assassin).RatingsThisYear;
            }
            else
            {
                ObservableCollection<KeyValuePair<string, int>> new_data_source = new ObservableCollection<KeyValuePair<string, int>>();
                new_data_source.Add(new KeyValuePair<string, int>(my_resourses.texts.kill_many_guards,
                    0));
                new_data_source.Add(new KeyValuePair<string, int>(my_resourses.texts.stealth_orders,
                    0));
                new_data_source.Add(new KeyValuePair<string, int>(my_resourses.texts.work_in_team,
                    0));
                new_data_source.Add(new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_poison,
                    0));
                new_data_source.Add(new KeyValuePair<string, int>(my_resourses.texts.kill_target_with_trap,
                    0));
                return new_data_source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    class list_box_item_to_5_years_diagram : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value != null)
            {
                return (value as great_assassin).RatingsLast5Years;
            }
            else
            {
                ObservableCollection<year_rating> new_data_source = new ObservableCollection<year_rating>();
                int buf_for_year = 2011;
                for (int counter = 0; counter < 5; ++counter)
                {
                    new_data_source.Add(new year_rating(buf_for_year.ToString(), 0));
                    ++buf_for_year;
                }
                return new_data_source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

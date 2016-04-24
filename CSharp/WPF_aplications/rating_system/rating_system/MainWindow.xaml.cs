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
using System.IO;
using Microsoft;
using System.Collections.ObjectModel;

namespace rating_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         

        public static Random main_random = new Random();
        public ObservableCollection<great_assassin> list_of_famous_assasins = new ObservableCollection<great_assassin>();

        public MainWindow()
        {
            InitializeComponent();
            
            // /WpfApplication1;component/Images/Untitled.png
            list_of_famous_assasins.Add(new great_assassin(@"/my_resourses/portraits/face_1.jpg",
                "Bob","Wield"));
            add_ratings_to_assasin(0);
            list_of_famous_assasins.Add(new great_assassin(@"/my_resourses/portraits/face_2.jpg",
                "Jim", "Garden"));
            add_ratings_to_assasin(1);
            list_of_famous_assasins.Add(new great_assassin(@"/my_resourses/portraits/face_3.jpg",
                "Alex", "Invisible"));
            add_ratings_to_assasin(2);
            fame_board_list_box.ItemsSource = list_of_famous_assasins;

        }

        public void add_ratings_to_assasin (int current_index)
        {
            list_of_famous_assasins[current_index].DefeatedManyGuards = main_random.Next(1, 600);
            list_of_famous_assasins[current_index].KilledWithPoison = main_random.Next(1, 600);
            list_of_famous_assasins[current_index].KilledWithTraps = main_random.Next(1, 600);
            list_of_famous_assasins[current_index].StealthOrders = main_random.Next(1, 600);
            list_of_famous_assasins[current_index].TeamWork = main_random.Next(1, 600);

            ObservableCollection<KeyValuePair<string, int>> ratings_last_5_years = new ObservableCollection<KeyValuePair<string, int>>();
            ObservableCollection<year_rating> ratings_for_1_year = new ObservableCollection<year_rating>();
            int buf_for_year = 2011;
            for (int counter = 0; counter < 4; ++counter)
            {
                ratings_last_5_years.Add(new KeyValuePair<string, int>(buf_for_year.ToString(), main_random.Next(1, 3000)));
                ++buf_for_year;
                ratings_for_1_year.Add(new year_rating(ratings_last_5_years[counter].Key, ratings_last_5_years[counter].Value));
            }
            ratings_last_5_years.Add(new KeyValuePair<string, int>(buf_for_year.ToString(), 
                list_of_famous_assasins[current_index].Fame));
            ratings_for_1_year.Add(new year_rating(ratings_last_5_years[4].Key, 
                list_of_famous_assasins[current_index].Fame));
            list_of_famous_assasins[current_index].RatingsLast5Years = ratings_for_1_year;
            list_of_famous_assasins[current_index].RatingsLast5Years_keyValue = ratings_last_5_years;
            list_of_famous_assasins[current_index].Education = my_resourses.texts.empty;
            list_of_famous_assasins[current_index].Rank = my_resourses.texts.assassin_rank;
            list_of_famous_assasins[current_index].Salary = "0";
            list_of_famous_assasins[current_index].assassinId = "0";
            list_of_famous_assasins[current_index].Location = my_resourses.texts.empty;
            ObservableCollection<operation> buf_for_operations = new ObservableCollection<operation>();
            for (int counter = 0; counter < 3; ++counter )
            {
                buf_for_operations.Add(new operation(my_resourses.texts.operation_name,
                    my_resourses.texts.description_for_operation));
            }
            list_of_famous_assasins[current_index].Operations = buf_for_operations;
        }

        private void change_image_button_Click(object sender, RoutedEventArgs e)
        {
            great_assassin test_assasin = (great_assassin)fame_board_list_box.SelectedItem;
            if ((great_assassin)fame_board_list_box.SelectedItem == null)
                MessageBox.Show(my_resourses.texts.selected_null,"", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                new change_image_window(this, fame_board_list_box.SelectedIndex, list_of_famous_assasins).ShowDialog();
        }

        private void add_new_assasin_button_Click(object sender, RoutedEventArgs e)
        {
            list_of_famous_assasins.Add(new great_assassin(@"/my_resourses/portraits/empty.png",
                "FirstName", "SecondName"));
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].DefeatedManyGuards = 0;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].KilledWithPoison = 0;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].KilledWithTraps = 0;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].StealthOrders = 0;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].TeamWork = 0;

            ObservableCollection<KeyValuePair<string, int>> ratings_last_5_years = new ObservableCollection<KeyValuePair<string, int>>();
            ObservableCollection<year_rating> ratings_for_1_year = new ObservableCollection<year_rating>();
            int buf_for_year = 2011;
            for (int counter = 0; counter < 4; ++counter)
            {
                ratings_last_5_years.Add(new KeyValuePair<string, int>(buf_for_year.ToString(), 0));
                ++buf_for_year;
                ratings_for_1_year.Add(new year_rating(ratings_last_5_years[counter].Key, ratings_last_5_years[counter].Value));
            }
            ratings_last_5_years.Add(new KeyValuePair<string, int>(buf_for_year.ToString(),0 ));
            ratings_for_1_year.Add(new year_rating(ratings_last_5_years[4].Key,0));
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].RatingsLast5Years = ratings_for_1_year;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].RatingsLast5Years_keyValue = ratings_last_5_years;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].Education = my_resourses.texts.empty;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].Rank = my_resourses.texts.assassin_rank;
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].Salary = "0";
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].assassinId = "0";
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].Location = my_resourses.texts.empty;
            ObservableCollection<operation> buf_for_operations = new ObservableCollection<operation>();
            list_of_famous_assasins[list_of_famous_assasins.Count - 1].Operations = buf_for_operations;

            fame_board_list_box.Items.Refresh();
        }

        private void delete_assasin_button_Click(object sender, RoutedEventArgs e)
        {
            if (fame_board_list_box.SelectedIndex >= 0)
            {
                list_of_famous_assasins.RemoveAt(fame_board_list_box.SelectedIndex);
                fame_board_list_box.Items.Refresh();
            }
            else
            {
                MessageBox.Show(my_resourses.texts.selected_null, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void delete_operation_button_Click(object sender, RoutedEventArgs e)
        {
            if (operations_list_box.SelectedIndex >= 0)
            {
                (fame_board_list_box.SelectedItem as great_assassin).Operations.RemoveAt(operations_list_box.
                    SelectedIndex);
                //operations_list_box.Items.Refresh();
            }
            else
            {
                MessageBox.Show(my_resourses.texts.selected_operation_null, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void add_operation_button_Click(object sender, RoutedEventArgs e)
        {
            if (fame_board_list_box.SelectedIndex >= 0)
            {
                (fame_board_list_box.SelectedItem as great_assassin).Operations.Add(new operation
                    (my_resourses.texts.operation_name,my_resourses.texts.description_for_operation));
                //operations_list_box.Items.Refresh();
            }
            else
            {
                MessageBox.Show(my_resourses.texts.selected_null, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void styles_combo_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Resources.MergedDictionaries.Clear();
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("my_resourses/visualization.xaml", UriKind.Relative) });
            }
            else
            {
                string test = (sender as ComboBox).SelectedValue.ToString();
                this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("my_resourses/" +
                    (sender as ComboBox).SelectedValue.ToString() + ".xaml", UriKind.Relative)});
            }
        }
    }
}

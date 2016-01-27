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
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management;

namespace TaskManager_on_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
            //CollectionViewSource test = new CollectionViewSource() { Source = Process.GetProcesses() };
            //test.SortDescriptions. = 
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (auto_refresh_check_box.IsChecked == true)
            {
                my_task_class buf_for_item;
                storage_for_tasks.main_storage_for_tasks = my_task_class.return_array_of_tasks();
                if (main_data_grid.SelectedIndex >= 0)
                {
                    buf_for_item = (my_task_class)main_data_grid.SelectedItem;
                    for (int counter = 0; counter < storage_for_tasks.main_storage_for_tasks.Length - 1; ++counter)
                    {
                        if (storage_for_tasks.main_storage_for_tasks[counter].current_id == buf_for_item.current_id &&
                            storage_for_tasks.main_storage_for_tasks[counter].process_name == buf_for_item.process_name)
                        {
                            main_data_grid.ItemsSource = storage_for_tasks.main_storage_for_tasks;
                            main_data_grid.SelectedItem = main_data_grid.Items[counter];
                            return;
                        }
                    }
                }
                main_data_grid.ItemsSource = storage_for_tasks.main_storage_for_tasks;
            }
        }

        public void find_button_click(object sender, EventArgs e)
        {
            my_task_class[] buf_for_tasks = storage_for_tasks.main_storage_for_tasks;
            int index_of_selected_item = -1;
            for (int counter = 0; counter < buf_for_tasks.Length; ++counter )
            {
                if (buf_for_tasks[counter].process_name == task_name_text_box.Text)
                {
                    index_of_selected_item = counter;
                    break;
                }
            }
            if (index_of_selected_item >= 0)
            {
                main_data_grid.ScrollIntoView(main_data_grid.Items[index_of_selected_item]);
                main_data_grid.SelectedItem = main_data_grid.Items[index_of_selected_item];
            }
            else
                MessageBox.Show(my_resourses.texts.task_dont_exist);
        }

        private void new_task_button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".exe";
            dlg.Filter = "Applications (*.exe)|*.exe";
            if (dlg.ShowDialog() == true)
            {
                Process.Start(dlg.FileName);
                storage_for_tasks.main_storage_for_tasks = my_task_class.return_array_of_tasks();
                main_data_grid.ItemsSource = storage_for_tasks.main_storage_for_tasks;
            }
        }

        private void end_task_button_Click(object sender, RoutedEventArgs e)
        {
            if (main_data_grid.SelectedIndex >= 0)
            {
                Process task_to_kill = Process.GetProcessById(Int32.Parse(main_data_grid.SelectedItem.ToString()));
                MessageBoxResult answer = MessageBox.Show(my_resourses.texts.do_you_wish_end_task + " " +
                    task_to_kill.ProcessName + "?", my_resourses.texts.warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if ( answer == MessageBoxResult.Yes )
                {
                    int buf_for_id = task_to_kill.Id;
                    task_to_kill.Kill();
                    while (true)
                    {
                        try
                        {
                            Process buf_for_tasks = Process.GetProcessById(buf_for_id);
                        }
                        catch
                        {
                            break;
                        }
                    }
                    storage_for_tasks.main_storage_for_tasks = my_task_class.return_array_of_tasks();
                    main_data_grid.ItemsSource = storage_for_tasks.main_storage_for_tasks;
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show(my_resourses.texts.bad_selected_index);
            }
        }

        private void filter_id_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<my_task_class> dynamic_collection = new List<my_task_class>();
            for (int counter = 0; counter < storage_for_tasks.main_storage_for_tasks.Length; ++counter)
            {
                if ((storage_for_tasks.main_storage_for_tasks[counter].current_id.ToString()).Contains(((TextBox)sender).Text))
                {
                    dynamic_collection.Add(storage_for_tasks.main_storage_for_tasks[counter]);
                }
            }
            if (dynamic_collection.Count > 0)
            {
                my_task_class[] template_source_of_tasks = new my_task_class[dynamic_collection.Count];
                int counter_for_array = 0;
                foreach (my_task_class cur_task in dynamic_collection)
                {
                    template_source_of_tasks[counter_for_array] = new my_task_class();
                    template_source_of_tasks[counter_for_array].current_id = cur_task.ID;
                    template_source_of_tasks[counter_for_array].process_name = cur_task.process_name;
                    template_source_of_tasks[counter_for_array].memory = cur_task.memory;
                    template_source_of_tasks[counter_for_array].user_name_for_task = cur_task.user_name_for_task;
                    ++counter_for_array;
                }
                main_data_grid.ItemsSource = template_source_of_tasks;
            }
        }

        private void filter_name_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<my_task_class> dynamic_collection = new List<my_task_class>();
            for (int counter = 0; counter < storage_for_tasks.main_storage_for_tasks.Length; ++counter)
            {
                if ((storage_for_tasks.main_storage_for_tasks[counter].process_name).Contains(((TextBox)sender).Text))
                {
                    dynamic_collection.Add(storage_for_tasks.main_storage_for_tasks[counter]);
                }
            }
            if (dynamic_collection.Count > 0)
            {
                my_task_class [] template_source_of_tasks = new my_task_class[dynamic_collection.Count];
                int counter_for_array = 0;
                foreach (my_task_class cur_task in dynamic_collection)
                {
                    template_source_of_tasks[counter_for_array] = new my_task_class();
                    template_source_of_tasks[counter_for_array].current_id = cur_task.ID;
                    template_source_of_tasks[counter_for_array].process_name = cur_task.process_name;
                    template_source_of_tasks[counter_for_array].memory = cur_task.memory;
                    template_source_of_tasks[counter_for_array].user_name_for_task = cur_task.user_name_for_task;
                    ++counter_for_array;
                }
                main_data_grid.ItemsSource = template_source_of_tasks;
            }
        }

        private void filter_user_name_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<my_task_class> dynamic_collection = new List<my_task_class>();
            for (int counter = 0; counter < storage_for_tasks.main_storage_for_tasks.Length; ++counter)
            {
                if ((storage_for_tasks.main_storage_for_tasks[counter].user_name_for_task).Contains(((TextBox)sender).Text))
                {
                    dynamic_collection.Add(storage_for_tasks.main_storage_for_tasks[counter]);
                }
            }
            if (dynamic_collection.Count > 0)
            {
                my_task_class[] template_source_of_tasks = new my_task_class[dynamic_collection.Count];
                int counter_for_array = 0;
                foreach (my_task_class cur_task in dynamic_collection)
                {
                    template_source_of_tasks[counter_for_array] = new my_task_class();
                    template_source_of_tasks[counter_for_array].current_id = cur_task.ID;
                    template_source_of_tasks[counter_for_array].process_name = cur_task.process_name;
                    template_source_of_tasks[counter_for_array].memory = cur_task.memory;
                    template_source_of_tasks[counter_for_array].user_name_for_task = cur_task.user_name_for_task;
                    ++counter_for_array;
                }
                main_data_grid.ItemsSource = template_source_of_tasks;
            }
        }

        private void text_box_only_digits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void auto_seconds_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int new_seconds = Int32.Parse(((TextBox)sender).Text);
                if (new_seconds == 0)
                    timer.Interval = new TimeSpan(0, 0, 5);
                else
                    timer.Interval = new TimeSpan(0, 0, new_seconds);
            }
            catch
            {
                timer.Interval = new TimeSpan(0, 0, 5);
            }
        }

        private void green_theme_button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("my_resourses/themes/green_scene.xaml", UriKind.Relative) });
        }

        private void blue_theme_button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("my_resourses/themes/blue_scene.xaml", UriKind.Relative) });
        }

        private void red_theme_button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("my_resourses/themes/red_scene.xaml", UriKind.Relative) });
        }

        private void gray_theme_button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources.MergedDictionaries.Clear();
        }
    }

    class storage_for_tasks
    {
        static public my_task_class[] main_storage_for_tasks = my_task_class.return_array_of_tasks();

        static public my_task_class[] get_all_tasks
        {
            get
            {
                return main_storage_for_tasks;
            }
        }

    }
}

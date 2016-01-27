using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Management;

namespace TaskManager_on_wpf
{

    class info_about_task_converter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                Process selected_task = Process.GetProcessById(((my_task_class)value).current_id);
                string buf_for_text;
                string buf_for_responce;
                    if (selected_task.Responding)
                    {
                        buf_for_responce = my_resourses.texts.state + " " + my_resourses.texts.responding + "\n";
                    }
                    else
                    {
                        buf_for_responce = my_resourses.texts.state + " " + my_resourses.texts.not_responding + "\n";
                    }
                buf_for_text = buf_for_responce + my_resourses.texts.priority + " " + selected_task.BasePriority.ToString() + "\n" +
                    my_resourses.texts.start_time + " " + selected_task.StartTime.ToString() + "\n" +
                    my_resourses.texts.total_processor_time + " " + selected_task.TotalProcessorTime.ToString() + "\n" +
                    my_resourses.texts.user_processor_time + " " + selected_task.UserProcessorTime.ToString() + "\n" +
                    my_resourses.texts.nonpaged_memory + " " + (((selected_task.NonpagedSystemMemorySize64) / 8) / 1024).ToString() +
                    " " + my_resourses.texts.kbyte + "\n" +
                    my_resourses.texts.paged_memory + " " + (((selected_task.PagedMemorySize64) / 8) / 1024).ToString() +
                    " " + my_resourses.texts.kbyte + "\n" +
                    my_resourses.texts.peak_virtual_memory + " " + (((selected_task.PeakVirtualMemorySize64) / 8) / 1024).ToString() +
                    " " + my_resourses.texts.kbyte;
                return buf_for_text;
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class height_converter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value) - 130.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class width_converter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value) / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class height_converter_for_run_task_image : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value)/2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class my_task_class
    {
        public int current_id = 0;
        public string process_name = "";
        public long memory = 0;
        public string user_name_for_task;

        public string UserName
        {
            get
            {
                return user_name_for_task;
            }
        }

        public int ID
        {
            get
            {
                return current_id;
            }
        }

        public string NameOfProcess
        {
            get
            {
                return process_name;
            }
        }

        public string Memory
        {
            get
            {
                long buf_for_memory = (memory / 8)/1024;
                return buf_for_memory + " " + my_resourses.texts.kbyte;
            }
        }

        public override string ToString()
        {
            return current_id.ToString();
        }

        static public my_task_class[] return_array_of_tasks()
        {
            Process[] buf_for_processes = Process.GetProcesses();
            my_task_class[] buf_for_my_tasks = new my_task_class[buf_for_processes.Length];
            int counter = 0;
            //*********************************************************************************
            SelectQuery query = new SelectQuery(@"SELECT * FROM Win32_Process");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                ManagementObjectCollection processes = searcher.Get();
                foreach (ManagementObject process in processes)
                {
                    process.Get();
                    string[] user_and_domain_names = new string[2];
                    user_and_domain_names[0] = " ";
                    user_and_domain_names[1] = " ";
                    process.InvokeMethod("GetOwner", user_and_domain_names);
                    if (user_and_domain_names[0] == null)
                        user_and_domain_names[0] = "";
                    PropertyDataCollection processProperties =
                         process.Properties;
                    foreach (Process cur_proc in buf_for_processes)
                    {
                        if (buf_for_my_tasks[counter] == null)
                        {
                            buf_for_my_tasks[counter] = new my_task_class();
                            buf_for_my_tasks[counter].memory = cur_proc.PrivateMemorySize64;
                            buf_for_my_tasks[counter].process_name = cur_proc.ProcessName;
                            buf_for_my_tasks[counter].current_id = cur_proc.Id;
                        }
                        int test_int = int.Parse(string.Format("{0}", processProperties["ProcessId"].Value));
                        if (buf_for_my_tasks[counter].current_id == int.Parse(string.Format("{0}", processProperties["ProcessId"].Value)))
                        {
                            buf_for_my_tasks[counter].user_name_for_task = user_and_domain_names[0];
                            break;
                        }
                        ++counter;
                    }
                    counter = 0;
                }
            }
            //*********************************************************************************
            return buf_for_my_tasks;
        }
    }

    class list_of_tasks_converter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Process[] buf_for_tasks = (Process[])value;
            my_task_class[] buf_for_my_tasks = new my_task_class[buf_for_tasks.Length];
            for (int counter = 0; counter < buf_for_tasks.Length - 1; ++counter)
            {
                buf_for_my_tasks[counter] = new my_task_class();
                buf_for_my_tasks[counter].current_id = buf_for_tasks[counter].Id;
            }
            return buf_for_my_tasks;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

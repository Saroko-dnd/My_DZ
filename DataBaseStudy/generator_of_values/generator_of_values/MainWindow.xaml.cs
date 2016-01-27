using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.IO;


namespace generator_of_values
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer my_timer;
        public int counter_of_rows = 0;
        public int amount_of_rows_to_add = 0;

        public MainWindow()
        {
            InitializeComponent();
            my_timer = new System.Windows.Threading.DispatcherTimer();
            my_timer.Tick += new EventHandler(dispatcherTimer_Tick);
            my_timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            amount_of_rows_to_add_TextBox.PreviewTextInput += text_box_only_digits_PreviewTextInput;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (amount_of_rows_to_add == 0)
                {
                    counter_of_rows = 0;
                    number_of_added_rows.Text = "0";
                    amount_of_rows_to_add = Int32.Parse(amount_of_rows_to_add_TextBox.Text);
                    if (amount_of_rows_to_add > 0)
                        my_timer.Start();
                }
            }
            catch
            {
                MessageBox.Show("Введите количество строк!","",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Random our_random = new Random();
            List<string> TV_names = new List<string>();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "generator_of_values.TV_names.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string buf = reader.ReadLine();
                while (buf != null)
                {
                    TV_names.Add(buf);
                    buf = reader.ReadLine();
                }
            }
            OleDbConnection connection_to_internet_shop_DB = new OleDbConnection("Provider=Microsoft.Ace.OLEDB.12.0;" +
                @"Data Source= ../../my_resourses/InternetShop.accdb");
            connection_to_internet_shop_DB.Open();
            OleDbCommand command_for_data_base = new OleDbCommand
                ("INSERT INTO Product (ProductName,Price,Weight,Length,Width,Thickness)" +
            "VALUES (?,?,?,?,?,?)", connection_to_internet_shop_DB);
            for (int counter = 0; counter < 1000; ++counter)
            {
                command_for_data_base.Parameters.Add(new OleDbParameter("ProductName",
                    TV_names[our_random.Next(0, TV_names.Count)]));
                command_for_data_base.Parameters.Add(new OleDbParameter("Price", our_random.Next(1200, 5600)));
                command_for_data_base.Parameters.Add(new OleDbParameter("Weight", our_random.Next(40, 200)));
                command_for_data_base.Parameters.Add(new OleDbParameter("Length", our_random.Next(40, 200)));
                command_for_data_base.Parameters.Add(new OleDbParameter("Width", our_random.Next(40, 200)));
                command_for_data_base.Parameters.Add(new OleDbParameter("Thickness", our_random.Next(40, 200)));
                command_for_data_base.ExecuteNonQuery();
                command_for_data_base.Parameters.Clear();
                ++counter_of_rows;
                if (counter_of_rows == amount_of_rows_to_add)
                {
                    amount_of_rows_to_add = 0;
                    my_timer.Stop();
                    break;
                }
            }
            number_of_added_rows.Text = counter_of_rows.ToString();
            connection_to_internet_shop_DB.Close();
        }

        private void text_box_only_digits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Example7._1DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> persons;
       
        public MainWindow()
        {
            InitializeComponent();

            persons = new ObservableCollection<Person>();
            persons.Add(new Person()
            {
                FirstName = "Ivan",
                EmployeeNumber = 1,
                Date = new DateTime(1983, 03, 30),
                Department = Department.Programmers,
                GoodBoy = true,
                Salory = 2500
            }
          );

            persons.Add(new Person()
            {
                FirstName = "Alex",
                EmployeeNumber = 2,
                Date = new DateTime(1975, 03, 30),
                Department = Department.Programmers,
                GoodBoy = true,
                Salory = 2500
            }
          );

            persons.Add(new Person()
            {
                FirstName = "Inna",
                EmployeeNumber = 3,
                Date = new DateTime(1998, 05, 15),
                Department = Department.Disigners,
                GoodBoy = true,
                Salory = 2500
            }
          );

            dgMain.ItemsSource = persons;

        }

        

       
    }
}

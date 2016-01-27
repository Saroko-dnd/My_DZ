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
        ObservableCollection<Person> collection;
        public MainWindow()
        {
            InitializeComponent();

            collection = new ObservableCollection<Person>();
            collection.Add(new Person() 
            {   FirstName = "Ivan", 
                EmployeeNumber = 1,
                Date=new DateTime(1983,03,30),
                Department=Department.Programmers,
                GoodBoy=true,
                Salory=2500
            }
           );

            collection.Add(new Person()
            {
                FirstName = "Alex",
                EmployeeNumber = 1,
                Date = new DateTime(1975, 03, 30),
                Department = Department.Programmers,
                GoodBoy = true,
                Salory = 2500
            }
          );

            collection.Add(new Person()
            {
                FirstName = "Inna",
                EmployeeNumber = 1,
                Date = new DateTime(1998, 05, 15),
                Department = Department.Disigners,
                GoodBoy = true,
                Salory = 2500
            }
          );


            ListCollectionView view = new ListCollectionView(collection);
            view.GroupDescriptions.Add(new PropertyGroupDescription("Department"));
            dgMain.ItemsSource = view;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(String.Format("В таблицы всего {0} элементов", collection.Count));
        }
    }
}

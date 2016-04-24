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

namespace EntityCodeFirst_TEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                CarsContext OurContext = new CarsContext("CarsConnectionString");

                Car NewCar = new Car() { ID = 7, Name = "CarName", Speed = 670.9/*, YearOfCreation = 1995 */};
                OurContext.Cars.Add(NewCar);
                OurContext.SaveChanges();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            //беру строку подключения из App.config

        }
    }
}

using DatabaseBusesEntityDB.DBContext;
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

namespace DatabaseBusesEntityDB
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
                TransportDBContext OurContext = new TransportDBContext("TransportConnectionString");
                OurContext.VehicleTypes.Add(new TypeOfVehicle() { TypeName = "tank" });
                OurContext.VehicleTypes.Add(new TypeOfVehicle() { TypeName = "bus" });
                OurContext.VehicleTypes.Add(new TypeOfVehicle() { TypeName = "trolleybus" });
                OurContext.SaveChanges();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }            
        }
    }
}

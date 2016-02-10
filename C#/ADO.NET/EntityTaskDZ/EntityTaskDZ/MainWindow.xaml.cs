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

namespace EntityTaskDZ
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
                ShopDBModelContainer ggg = new ShopDBModelContainer();
                Instructor OurInstructor = new Instructor() 
                { 
                    FirstMidName = "FirstMidTest", 
                    LastName = "LastNameTest",
                    HireDate = ((new DateTime(1999, 4, 23)) as DateTime?), 
                };
                if (((new DateTime(1999, 4, 23)) as DateTime?).HasValue)
                {
                    MessageBox.Show("DateTime NOT NULL!!");
                }
                OfficeAssignment OurOfficeAssignment = new OfficeAssignment()
                {
                    Location = "LocationTest",
                    Instructor = OurInstructor
                };
                OurInstructor.OfficeAssignment = OurOfficeAssignment;

                ggg.OfficeAssignmentSet.Add(OurOfficeAssignment);
                ggg.SaveChanges();
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message + "/////" + 
                    CurrentException.InnerException.Message);
            }

        }
    }
}

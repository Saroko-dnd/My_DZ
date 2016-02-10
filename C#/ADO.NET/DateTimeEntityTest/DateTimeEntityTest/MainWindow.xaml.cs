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

namespace DateTimeEntityTest
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
                ModelWithDateTimeContainer ggg = new ModelWithDateTimeContainer();
                Jobs job = new Jobs()
                {
                    JobName = "TestName",
                    Peoples = new Peoples() { BirthDate = new DateTime(1987, 8, 12) }
                };
                ggg.JobsSet.Add(job);
                ggg.SaveChanges();
            }
            catch(Exception CurExc)
            {
                MessageBox.Show(CurExc.Message);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data.Entity;

namespace EntityFrameworkFirstTry_VS2013
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NorthwindEntities OurLocalDataProvider;

        public MainWindow()
        {
            InitializeComponent();

            OurLocalDataProvider = new NorthwindEntities();
            //
            CustomersDataGrid.DataContext = OurLocalDataProvider.Customers.Where(res => res.Country == "Mexico").
                ToList();

        }

        public void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            OurLocalDataProvider.Dispose();
        }

       //Асинхронное сохранение изменений и выполнение запросов
        async public void SaveChangesToDB()
        {
            int ChangedRows = await OurLocalDataProvider.SaveChangesAsync();

            CustomersDataGrid.DataContext = await OurLocalDataProvider.Customers.
                Where(res => res.Country == "Mexico").ToListAsync();
        }
    }
}

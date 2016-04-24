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
            //Query to model of database (example)
            //-------------------------------------
            CustomersDataGrid.DataContext = OurLocalDataProvider.Customers.
                Where(res => res.Country == "Mexico").
                ToList();
            //-------------------------------------

            //adding new row to database in table 'Customers' (example)
            //--------------------------------------------------------
            try
            {
                Customers NewCustomer = new Customers()
                {
                    Address = "TestAddress",
                    City = "Test",
                    CompanyName = "Test",
                    ContactName = "Test",
                    ContactTitle = "Test",
                    Country = "Test",
                    CustomerID = "Test",
                    Fax = "Test",
                    Phone = "Test",
                    PostalCode = "Test",
                    Region = "Test"
                };
                OurLocalDataProvider.Customers.Add(NewCustomer);
                OurLocalDataProvider.SaveChanges();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            //--------------------------------------------------------
            Model1Container CreatedDB = new Model1Container();
            CreatedDB.JobsSet.Add(new Jobs() {  Job = "SuperJob", Salary = 10000 });
            CreatedDB.JobsSet.Add(new Jobs() { Job = "MegaJob", Salary = 15000 });
            CreatedDB.JobsSet.Add(new Jobs() { Job = "EpicJob", Salary = 20000 });
            CreatedDB.PeoplesSet.Add(new Peoples() { JobId = 1, Email = "Email_1", Name = "Name_1", SpecialCode = 19 });
            CreatedDB.PeoplesSet.Add(new Peoples() { JobId = 1, Email = "Email_2", Name = "Name_2", SpecialCode = 67 });
            CreatedDB.PeoplesSet.Add(new Peoples() { JobId = 1, Email = "Email_3", Name = "Name_3", SpecialCode = 34 });
            CreatedDB.SaveChanges();
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

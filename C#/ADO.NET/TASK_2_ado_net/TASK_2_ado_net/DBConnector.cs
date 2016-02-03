using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Data;
using System.Collections.ObjectModel;

namespace TASK_2_ado_net
{
    class DBConnector
    {

        public static SqlConnection ConnectionToDB;

        public static ObservableCollection<DataClassFirstQuery> FirstQueryDataCollection = new ObservableCollection<DataClassFirstQuery>();

        static DBConnector()
        {
            try
            {
                ConnectionToDB = new SqlConnection();
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionToDB.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionToNorthwind"].
                    ConnectionString;
            }
            catch
            {
                MessageBox.Show(MyResourses.Texts.ConnectionError, MyResourses.Texts.Error,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async void ExecuteFirstQuery()
        {
            try
            {
                ConnectionToDB.Open();
                SqlCommand CommandFirstQuery = new SqlCommand(MyResourses.SSQLCommands.FirstQuery, 
                    ConnectionToDB);
                IAsyncResult OperationResult = CommandFirstQuery.BeginExecuteReader();
                while (!OperationResult.IsCompleted)
                {
                    await Task.Delay(100);
                }
                FirstQueryDataCollection.Clear();
                SqlDataReader FirstQueryReader = CommandFirstQuery.EndExecuteReader(OperationResult);
                while (FirstQueryReader.Read() != false)
                {
                    FirstQueryDataCollection.Add(new DataClassFirstQuery()
                    {
                        CompanyName = FirstQueryReader[0].ToString(),
                        ContactName = FirstQueryReader[1].ToString(),
                        Address = FirstQueryReader[2].ToString(),
                        City = FirstQueryReader[3].ToString(),
                        Country = FirstQueryReader[4].ToString(),
                        Phone = FirstQueryReader[5].ToString(),
                        RequiredDate = FirstQueryReader[6].ToString(),
                        ShippedDate = FirstQueryReader[7].ToString(),
                        Freight = FirstQueryReader[8].ToString(),
                        ShipCity = FirstQueryReader[9].ToString(),
                        ShipRegion = FirstQueryReader[10].ToString(),
                        ShipCountry = FirstQueryReader[11].ToString(),
                        Quantity = FirstQueryReader[12].ToString(),
                        UnitPrice = FirstQueryReader[13].ToString(),
                        Discount = FirstQueryReader[14].ToString(),
                    });
                }
                FirstQueryReader.Close();
            }
            catch(Exception excep)
            {

                MessageBox.Show(MyResourses.Texts.CantAccessDB + excep.Message,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (ConnectionToDB.State == ConnectionState.Open)
                    ConnectionToDB.Close();
            }
        }
    }
}
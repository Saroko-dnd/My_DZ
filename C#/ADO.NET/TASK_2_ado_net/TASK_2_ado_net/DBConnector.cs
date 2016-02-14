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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.Linq;

namespace TASK_2_ado_net
{
    class DBConnector
    {

        public static SqlConnection ConnectionToDB;

        private static void FinalBlock(bool ResultOfOperation, ProgressBar indicator, Label TextIndicator)
        {
            if (ConnectionToDB.State == ConnectionState.Open)
                ConnectionToDB.Close();
            indicator.Visibility = Visibility.Hidden;
            if (ResultOfOperation)
                TextIndicator.Content = MyResourses.Texts.ProgramReady;
            else
                TextIndicator.Content = MyResourses.Texts.ProgramException;
        }

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

        public static async void ExecuteFirstQuery(DataGrid DataGridForResult ,ProgressBar indicator,Label TextIndicator)
        {
            bool success = true;
            try
            {
                TextIndicator.Content = MyResourses.Texts.ProgramBusy;
                indicator.Visibility = Visibility.Visible;
                await ConnectionToDB.OpenAsync();
                SqlCommand CommandFirstQuery = new SqlCommand(MyResourses.SSQLCommands.FirstQuery, 
                    ConnectionToDB);
                List<OurDataClass> BufForData = new List<OurDataClass>();
                SqlDataReader FirstQueryReader = await CommandFirstQuery.ExecuteReaderAsync();
                while (await FirstQueryReader.ReadAsync())
                {
                    BufForData.Add(new OurDataClass()
                    {
                        CompanyName = FirstQueryReader[0].ToString(),
                        ContactName = FirstQueryReader[1].ToString(),
                        Address = FirstQueryReader[2].ToString(),
                        City = FirstQueryReader[3].ToString(),
                        Country = FirstQueryReader[4].ToString(),
                        Phone = FirstQueryReader[5].ToString(),
                        OrderDate = FirstQueryReader[6].ToString(),
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
                DataGridForResult.ItemsSource = BufForData.Select(res => new
                {
                    res.CompanyName,
                    res.ContactName,
                    res.Address,
                    res.City,
                    res.Country,
                    res.Phone,
                    res.OrderDate,
                    res.ShippedDate,
                    res.Freight,
                    res.ShipCity,
                    res.ShipRegion,
                    res.ShipCountry,
                    res.Quantity,
                    res.UnitPrice,
                    res.Discount
                }).ToList();
            }
            catch(Exception excep)
            {
                MessageBox.Show(MyResourses.Texts.CantAccessDB + excep.Message,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }
            finally
            {
                FinalBlock(success, indicator, TextIndicator);
            }
        }

        public static async void ExecuteSecondQuery(DataGrid DataGridForResult, ProgressBar indicator, Label TextIndicator)
        {
            bool success = true;
            try
            {
                List<OurDataClass> BufForDataWithPhoto = new List<OurDataClass>();
                int offset = 78;
                TextIndicator.Content = MyResourses.Texts.ProgramBusy;
                indicator.Visibility = Visibility.Visible;
                await ConnectionToDB.OpenAsync();
                SqlCommand CommandSecondQuery = new SqlCommand(MyResourses.SSQLCommands.SecondQuery,
                    ConnectionToDB);
                SqlDataReader SecondQueryReader = await CommandSecondQuery.ExecuteReaderAsync();
                while (await SecondQueryReader.ReadAsync())
                {
                    Stream StreamForPhoto = new MemoryStream();
                    StreamForPhoto.Write((byte [])SecondQueryReader[5], offset, 
                        ((byte[])SecondQueryReader[5]).Length - offset);
                    BitmapImage CurrentPhoto = new BitmapImage();
                    CurrentPhoto.BeginInit();
                    CurrentPhoto.StreamSource = StreamForPhoto;
                    CurrentPhoto.EndInit();
                    BufForDataWithPhoto.Add(new OurDataClass() { Photo = CurrentPhoto,
                        LastName = SecondQueryReader[0].ToString(),
                        FirstName = SecondQueryReader[1].ToString(),
                        BirthDate = SecondQueryReader[2].ToString(),
                        Address = SecondQueryReader[3].ToString(),
                        HomePhone = SecondQueryReader[4].ToString()
                    });
                }
                SecondQueryReader.Close();
                DataGridForResult.ItemsSource = BufForDataWithPhoto;
            }
            catch (Exception excep)
            {

                MessageBox.Show(MyResourses.Texts.CantAccessDB + excep.Message,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }
            finally
            {
                FinalBlock(success, indicator, TextIndicator);
            }
        }

        public static async void ExecuteThirdQuery(DataGrid DataGridForResult, ProgressBar indicator, Label TextIndicator)
        {
            bool success = true;
            try
            {
                List<OurDataClass> BufForData = new List<OurDataClass>();
                TextIndicator.Content = MyResourses.Texts.ProgramBusy;
                indicator.Visibility = Visibility.Visible;
                await ConnectionToDB.OpenAsync();
                SqlCommand CommandSecondQuery = new SqlCommand(MyResourses.SSQLCommands.ThirdQuery,
                    ConnectionToDB);
                SqlDataReader SecondQueryReader = await CommandSecondQuery.ExecuteReaderAsync();
                while (await SecondQueryReader.ReadAsync())
                {
                    BufForData.Add(new OurDataClass()
                    {
                        ProductName = SecondQueryReader[0].ToString(),
                        UnitPrice = SecondQueryReader[1].ToString(),
                        Discontinued = SecondQueryReader[2].ToString(),
                        QuantityPerUnit = SecondQueryReader[3].ToString(),
                        CategoryName = SecondQueryReader[4].ToString()
                    });
                }
                SecondQueryReader.Close();
                DataGridForResult.ItemsSource = BufForData.Select(res => new { res.ProductName,
                    res.UnitPrice,res.Discontinued,res.QuantityPerUnit,res.CategoryName}).ToList();
            }
            catch (Exception excep)
            {

                MessageBox.Show(MyResourses.Texts.CantAccessDB + excep.Message,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }
            finally
            {
                FinalBlock(success, indicator, TextIndicator);
            }
        }

        public static async void ExecuteFourthQuery(TextBox KeyWordTextBox,Label ResultLabel, 
            ProgressBar indicator, Label TextIndicator)
        {
            bool success = true;
            try
            {
                TextIndicator.Content = MyResourses.Texts.ProgramBusy;
                indicator.Visibility = Visibility.Visible;
                await ConnectionToDB.OpenAsync();
                SqlCommand CommandSecondQuery = new SqlCommand(MyResourses.SSQLCommands.FourthQuery + "'" +
                    KeyWordTextBox.Text + "'", ConnectionToDB);
                int AmountOfCustomers = (int)await CommandSecondQuery.ExecuteScalarAsync();
                ResultLabel.Content = AmountOfCustomers.ToString();
            }
            catch (Exception excep)
            {

                MessageBox.Show(MyResourses.Texts.CantAccessDB + excep.Message,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }
            finally
            {
                FinalBlock(success, indicator, TextIndicator);
            }
        }

        public static async void ExecuteFifthQuery(TextBox DayFirstTextBox, TextBox MonthFirstTextBox, 
            TextBox YearFirstTextBox, TextBox DaySecondTextBox, TextBox MonthSecondTextBox, 
            TextBox YearSecondTextBox,DataGrid DataGridForResult, ProgressBar indicator, Label TextIndicator)
        {
            bool success = true;
            try
            {
                List<OurDataClass> BufForData = new List<OurDataClass>();
                TextIndicator.Content = MyResourses.Texts.ProgramBusy;
                indicator.Visibility = Visibility.Visible;
                await ConnectionToDB.OpenAsync();
                string QueryBuf = MyResourses.SSQLCommands.FifthQuery
                    .Replace("1_", DayFirstTextBox.Text).Replace("2_", MonthFirstTextBox.Text).
                    Replace("3_", YearFirstTextBox.Text).Replace("4_", DaySecondTextBox.Text).
                    Replace("5_", MonthSecondTextBox.Text).Replace("6_", YearSecondTextBox.Text);
                SqlCommand CommandSecondQuery = new SqlCommand(QueryBuf, ConnectionToDB);
                SqlDataReader ReaderForOrdersClientsInfo = await CommandSecondQuery.ExecuteReaderAsync();
                while (await ReaderForOrdersClientsInfo.ReadAsync())
                {
                    BufForData.Add(new OurDataClass()
                    {
                        CompanyName = ReaderForOrdersClientsInfo[0].ToString(),
                        City = ReaderForOrdersClientsInfo[1].ToString(),
                        Country = ReaderForOrdersClientsInfo[2].ToString(),
                        SumOrdersPrice = ReaderForOrdersClientsInfo[3].ToString()
                    });
                }
                DataGridForResult.ItemsSource = BufForData.Select(res => new {
                    res.CompanyName,
                    res.City,
                    res.Country,
                    res.SumOrdersPrice,
                }).ToList();
            }
            catch (Exception excep)
            {

                MessageBox.Show(MyResourses.Texts.CantAccessDB + excep.Message,
                    MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }
            finally
            {
                if (ConnectionToDB.State == ConnectionState.Open)
                    ConnectionToDB.Close();
                indicator.Visibility = Visibility.Hidden;
                if (success)
                    TextIndicator.Content = MyResourses.Texts.ProgramReady;
                else
                    TextIndicator.Content = MyResourses.Texts.ProgramException;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Data;

namespace TASK_2_ado_net
{
    class DBConnector
    {
        public static SqlConnection ConnectionToDB;

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
    }
}

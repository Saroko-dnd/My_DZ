using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ExampleSQLServer2
{
    class Program
    {
        static void Main(string[] args)
        {

          
            SqlConnection connection = new SqlConnection();
            try
            {
                // получение информации о строке подключения из файла конфигурации! 

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
                
                connection.Open();
          
                SqlCommand command = new SqlCommand("SELECT * FROM Persons", connection);
           
                SqlDataReader reader = command.ExecuteReader();
          
                while (reader.Read() != false)
                {
                     Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader["Id"], reader["NAME"], reader["SALARY"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                
                connection.Close();
            }

            Console.ReadKey();
        }
    }
}

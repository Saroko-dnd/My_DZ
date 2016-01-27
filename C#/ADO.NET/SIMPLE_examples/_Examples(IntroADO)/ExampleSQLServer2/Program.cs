using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSQLServer2
{


    class Program
    {
        static void Main(string[] args)
        {
            // Cоздаём объект для соединения с базой MS SQL SERVER
            SqlConnection connection = new SqlConnection();
            try
            {
                // Прописываем строку соединения 
                SqlConnectionStringBuilder conString = new SqlConnectionStringBuilder();
                conString.DataSource=@"(localdb)\v11.0";
                conString.AttachDBFilename = @"D:\KIN\DB\People.mdf";
                conString.IntegratedSecurity = true;
                conString.ConnectTimeout = 50;
                connection.ConnectionString = conString.ToString();
                // Открываем соединение 
                connection.Open();
                // Создаём объект для исполнения запроса
                SqlCommand command = new SqlCommand("SELECT * FROM Persons", connection);
                // Исполняем запрос и сохраняем ссылку на объект результата
                SqlDataReader reader = command.ExecuteReader();
                // Проходимся по результатам работы запроса строка за строкой
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
                // Закрываем соединение
                connection.Close();
            }

            Console.ReadKey();
        }
    }
}

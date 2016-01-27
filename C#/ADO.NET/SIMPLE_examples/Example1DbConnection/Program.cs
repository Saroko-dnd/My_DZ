using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// подключение пространства имен для работы  с ADO

using System.Data.SqlClient;
using System.Data;

namespace Example1DBConnection
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnection connection = new SqlConnection();
            try
            {   // Установка строк, используемой для открытия  подключения. 
                connection.ConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";
                
                // Получение времени ожидания установки подключения до завершения попыток подключения и генерирования ошибки
                Console.WriteLine("Время ожидания: " + connection.ConnectionTimeout); 
                // Получение имени текущей базы данных после открытия подключения (имя базы данных, указанное в строке подключения до его открытия)
                Console.WriteLine(connection.Database);
                //  Получение имени сервера, к которому производиться подключение. 
                Console.WriteLine(connection.DataSource);
               
                //  Получение  строки, описывающей подключение.
                Console.WriteLine(connection.State);

                // Открывает подключение к базе данных с настройками, указанными 
                // в System.Data.Common.DbConnection.ConnectionString
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine(connection.ServerVersion); // версия сервера
                    Console.WriteLine(connection.WorkstationId); // Имя ПЭВМ
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(connection.State);
            }
            finally
            {
                // Закрывает текущее подключение. 
                connection.Close();
                Console.WriteLine(connection.State);
            }

            Console.ReadKey();
        }
    }
}


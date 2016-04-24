using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// подключение пространства имен для работы  с ADO

using System.Data.SqlClient;
using System.Data;

namespace ExampleSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
           
            SqlConnection connection = new SqlConnection();
            try
            {

                // Дополнительны параметры в строке подключения:
                // Timeout - тайм-аут ожидания подключения

                connection.ConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True;Timeout=3";
                // Открываем соединение 
                Console.WriteLine("Время ожидания: "+connection.ConnectionTimeout); // время ожидания подключения
               
                Console.WriteLine(connection.Database);
                Console.WriteLine(connection.DataSource);
                
                // ConnectionState: 
                // Open         -   Подключение открыто.
                // Closed       -   Подключение закрыто.
                // Connecting   -   Объект подключения подключается к источнику данных.
                // Executing    -   Объект подключения выполняет команду.
                // Fetching     -   Объект подключения получает данные.  
                // Broken       -   Подключение к источнику данных разорвано.
                                    // Это может произойти только после открытия подключения.
                                    // Подключение в этом режиме может быть закрыто, а затем повторно открыто.
                
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine(connection.ServerVersion);
                    Console.WriteLine(connection.WorkstationId);
                }
                Console.WriteLine(connection.State);
                
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine(connection.ServerVersion);
                    Console.WriteLine(connection.WorkstationId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(connection.State);
            }
            finally
            {
                // Закрываем соединение
                connection.Close();
                Console.WriteLine(connection.State);
            }

            Console.ReadKey();
        }
    }
}

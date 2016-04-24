using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// подключение пространства имен для работы  с ADO

using System.Data.SqlClient;

namespace ExampleSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // создаём объект для соединения с базой MS SQL SERVER
            SqlConnection connection = new SqlConnection();
            try
            {
                // Прописываем строку соединения 
                // Указываем строку подключения к базе MS SQL SERVER
                // Параметры: 
                // Data Source=KIN-PC\SQLEXPRESS - имя сервера 
                // Initial Catalog=People - имя базы данных
                // Integrated Security=True 
                connection.ConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";
          
                // Открываем соединение 
                connection.Open();
                // Создаём объект для исполнения запроса
                // Указываем ему в качестве параметра запрос и объект открытого соединения
                SqlCommand command = new SqlCommand("SELECT * FROM Persons", connection);
                // Исполняем запрос и сохраняем ссылку на объект результата
                SqlDataReader reader = command.ExecuteReader();
                // Проходимся по результатам работы запроса строка за строкой
                // Когда данные кончатся метод Read вернет false
                while (reader.Read() != false)
                {
                    // Так как в таблице три столбца в результате в строке будет тоже три столбца
                    // Для обращения к столбцу используется индексатор 
                    // Возможен доступ как по имени столбца так и по индексу
                    Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader["Id"], reader["NAME"], reader["SALARY"]);
                    // Console.WriteLine("{0,-10} {1,-10} {2,-10}", reader[0], reader[1], reader[2]);
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

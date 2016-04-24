using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example3DbDataReader
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";

                connection.Open();

             
                // Оператор SQL - вставка данных в таблицу
                string cmdInsert = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values ('Ivan', 'C# .NET', 200.2 ) ";
                SqlCommand commandInsert = connection.CreateCommand();
                commandInsert.CommandText = cmdInsert;
                commandInsert.ExecuteNonQuery();
                cmdInsert = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values ('Igor', 'C++', 280.8) ";
                commandInsert.CommandText = cmdInsert;
                commandInsert.ExecuteNonQuery();
                cmdInsert = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values ('Alex', 'Java', 150.9 ) ";
                commandInsert.CommandText = cmdInsert;
                commandInsert.ExecuteNonQuery();


                // Оператор SQL - получение значения из таблицы
                string cmdSelectData = @"SELECT * FROM Persons";
                SqlCommand commandSelect = new SqlCommand(cmdSelectData, connection);
                //  Выполняет оператор SQL применительно к объекту подключения
                //  Возвращает объект DbDataReader.   
                DbDataReader dataReader = commandSelect.ExecuteReader();
                             
                while(dataReader.Read())
                {
                    String info = String.Format("{0}. {1}{2} - {3}$", dataReader["Id"],
                                                                      dataReader["NAME"],
                                                        // GetString() - Получает значение заданного столбца в виде экземпляра класса String.
                                                                      dataReader.GetString(2),
                                                        // GetDouble () Получает значение заданного столбца в виде числа двойной точности с плавающей запятой.
                                                        // GetOrdinal()  Получает порядковый номер столбца при наличии заданного имени столбца.
                                                                      dataReader.GetDouble(dataReader.GetOrdinal("SALARY")));
                    Console.WriteLine(info);
                }
                dataReader.Close();

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

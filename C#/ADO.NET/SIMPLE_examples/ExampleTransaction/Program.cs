using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTransaction
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

                //  Начинаем транзакцию – получаем объект SqlTransaction
                SqlTransaction trans = connection.BeginTransaction();
                
       // ОПЕРАЦИЯ УДАЛЕНИЯ ВСЕХ ЗАПИСЕЙ 
                string cmdDele = @"Delete Persons";
                SqlCommand commandDelete = connection.CreateCommand();
                commandDelete.CommandText = cmdDele;
                // Включение команды в транзакцию
                commandDelete.Transaction = trans;
                commandDelete.ExecuteNonQuery();

     // ОПЕРАЦИИ ДОБАВЛЕНИЯ ЗАПИСЕЙ
                              
                string cmdInsert = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values ('Ivan', 'C# .NET', 200.2 ) ";
                SqlCommand commandInsert = connection.CreateCommand();
                commandInsert.CommandText = cmdInsert;
                // Включение команды в транзакцию
                commandInsert.Transaction = trans;
                commandInsert.ExecuteNonQuery();

                cmdInsert = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values ('Igor', 'C++', 280.8) ";
                commandInsert.CommandText = cmdInsert;
                // Включение команды в транзакцию
                commandInsert.Transaction = trans;
                commandInsert.ExecuteNonQuery();

                cmdInsert = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values ('Alex', 'Java', 150.9 ) ";
                commandInsert.CommandText = cmdInsert;
                // Включение команды в транзакцию
                commandInsert.Transaction = trans;
                commandInsert.ExecuteNonQuery();


                Console.WriteLine("Применить операции ТРАНЗАКЦИИ? 1-Да, Другое - нет! ");
                Console.Write("Введите ответ: ");
                int rez = 0;
                if (Int32.TryParse(Console.ReadLine(), out rez))
                {
                    if(rez == 1 )trans.Commit();
                      else trans.Rollback();
                }
                else
                {
                    trans.Rollback();
                }
                

                // Оператор SQL - получение значения из таблицы
                string cmdSelectData = @"SELECT * FROM Persons";
                SqlCommand commandSelect = new SqlCommand(cmdSelectData, connection);
                //  Выполняет оператор SQL применительно к объекту подключения
                //  Возвращает объект DbDataReader.   
                DbDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    String info = String.Format("{0}. {1}{2} - {3}$", dataReader["Id"],
                                                                      dataReader["NAME"],
                                                                      dataReader["SUBJECT"],
                                                                      dataReader["SALARY"]);
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

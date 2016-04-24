using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// подключение пространства имен для работы  с ADO

using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Example2DBCommand
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

                // Оператор SQL - создание таблицы в базе данных
                //string cmdCreateTable = @"CREATE TABLE Persons(Id INT IDENTITY, NAME VARCHAR(10), SUBJECT VARCHAR(20), SALARY FLOAT)";
                // Cоздание объекта класс SqlCommand (SQL запрос, текущее подключение)
                //SqlCommand commandCreate = new SqlCommand(cmdCreateTable, connection);
                //int NCr = commandCreate.ExecuteNonQuery();  // возвращает кол-во измененных строк
                //Console.WriteLine("Результат {0}", NCr);

                // Оператор SQL - вставка данных в таблицу
                string cmdInsert = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values ('Ivan', 'C# .NET', 200.2 ) ";
                // создание объекта класс SqlCommand
                SqlCommand commandInsert = connection.CreateCommand();
                commandInsert.CommandText = cmdInsert;
                // Выполняет оператор SQL применительно к объекту подключения
                // Возвращает кол-во обработанных  строк
                int NIns = commandInsert.ExecuteNonQuery();
                Console.WriteLine("Кол-во обработанных строк (Insert): {0}", NIns);


                // Оператор SQL - вставка данных в таблицу
                string cmdInsertParam = @"Insert Into Persons(NAME, SUBJECT, SALARY) Values (@NAMEIn, @SUBJECTIn, @SALARYIn) ";
                // создание объекта класс SqlCommand
                SqlCommand commandInsertParam = connection.CreateCommand();
                commandInsertParam.CommandText = cmdInsertParam;
                commandInsertParam.Parameters.AddWithValue("@NAMEIn", "Igor");
                commandInsertParam.Parameters.AddWithValue("@SUBJECTIn", "JAVA");
                commandInsertParam.Parameters.AddWithValue("@SALARYIn", "350.25");
                NIns = commandInsertParam.ExecuteNonQuery();
                Console.WriteLine("Кол-во обработанных  строк (Insert Param): {0}", NIns);
                

                // Оператор SQL - получение значения из таблицы
                string cmdSelectData = @"SELECT COUNT(*) FROM Persons";
                SqlCommand commandSelectFirst = new SqlCommand(cmdSelectData, connection);
                // Выполняет запрос и возвращает первый столбец первой 
                // строки результирующего набора, возвращаемого запросом. 
                // Все другие столбцы и строки игнорируются.  
                int NumRows=(int)commandSelectFirst.ExecuteScalar();
                Console.WriteLine("Кол-во строк : {0}", NumRows);


                // Оператор SQL - получение значений из таблицы
                string cmdSelectDataAll = @"SELECT * FROM Persons";
                SqlCommand commandSelect = new SqlCommand(cmdSelectDataAll, connection);
                //  Выполняет оператор SQL применительно к объекту подключения
                //  Возвращает объект DbDataReader.   
                DbDataReader dataReader = commandSelect.ExecuteReader();
                Console.WriteLine("Данные получены!");
                while (dataReader.Read()!=false)
                {
                    Console.WriteLine("\tName={0}, Salaty={1} $", dataReader["NAME"], dataReader["SALARY"]);
                }
                dataReader.Close();


                // Оператор SQL - обновление  данных в таблицe
                string cmdUpdate = @"Update Persons Set SUBJECT = 'C++' Where NAME = 'Igor'";
                SqlCommand commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText = cmdUpdate;
                int NUpd = commandUpdate.ExecuteNonQuery();
                Console.WriteLine("Кол-во обработанных  строк (Update): {0}", NIns);


                // Оператор SQL - удаление  данных из таблицы
                string cmdDelete = @"Delete from Persons where NAME = 'Ivan'";
                SqlCommand commandDelete = connection.CreateCommand();
                commandDelete.CommandText = cmdDelete;
                int NDel = commandDelete.ExecuteNonQuery();
                Console.WriteLine("Кол-во удаленных строк (Delete): {0}", NIns);

                NumRows = (int)commandSelectFirst.ExecuteScalar();
                Console.WriteLine("Кол-во строк : {0}", NumRows);


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


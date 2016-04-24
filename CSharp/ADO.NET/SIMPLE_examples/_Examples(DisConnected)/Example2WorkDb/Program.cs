using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Example2WorkDb
{
    class Program
    {
        static void Main(string[] args)
        {

            // Создание строки подключения
            string sConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";
           

            // Создание адаптера с выполнение запроса и передачений строки подключения
            // при этом для выполнения запроса открывается соединение.
            SqlDataAdapter adapterPersons = new SqlDataAdapter(@"Select * From Persons", sConnectionString);

            // создание таблицы Persons
            DataTable dtPersons = new DataTable("Persons");
            // заполнение таблицы Persons данными из адаптера
            adapterPersons.Fill(dtPersons);

            // вывод полученных значений
            foreach (DataRow drCurrent in dtPersons.Rows)
            {
                Console.WriteLine("{0} {1}", drCurrent["Name"].ToString(),
                                                drCurrent["Subject"].ToString());
            }

            // Формирование новых данных 
            DataRow newRow = dtPersons.NewRow();
            newRow["NAME"] = "Alex";
            newRow["SUBJECT"] = "ASP.NET";
            newRow["SALARY"] = 460.25;
            // Добавление новых данных в таблицу dtPersons
            dtPersons.Rows.Add(newRow);


            // Формирование команды обновления cmdInsert (+ получение id добавленной записи)
            string cmdInsert = @"Insert Persons  Values (@NAME, @SUBJECT, @SALARY ) SET @NewPersonId =  @@IDENTITY;";
            // инициализация InsertCommand командой cmdInsert и передача параметров соединения с базой
            adapterPersons.InsertCommand = new SqlCommand(cmdInsert, adapterPersons.SelectCommand.Connection);

            // Формирование входных параметров при выполнении команды cmdInsert
            SqlParameterCollection insertParameters = adapterPersons.InsertCommand.Parameters;
            insertParameters.Add("NAME",    SqlDbType.VarChar, 10, "NAME");
            insertParameters.Add("SUBJECT", SqlDbType.VarChar, 20, "SUBJECT");
            insertParameters.Add("SALARY",           // имя параметра 
                                 SqlDbType.Float,    // тип данных в источнике 
                                 0,                  // длина столбца
                                 "SALARY");          // имя столбца в источнике данных            

            // Формировавание и настройка выходныго параметра
            SqlParameter outputParameter = insertParameters.Add("NewPersonId", SqlDbType.Int);
            outputParameter.Direction = ParameterDirection.Output;

            Console.WriteLine("Значение выходного параметра: " + outputParameter.Value);
            Console.WriteLine("Выполнение обновления! ");
            
            // Обновление таблицы в базе данных
            adapterPersons.Update(dtPersons);
            Console.WriteLine("Значение выходного параметра: " + outputParameter.Value);

            dtPersons.Clear();
            adapterPersons.Fill(dtPersons);

            // вывод полученных значений
            foreach (DataRow drCurrent in dtPersons.Rows)
            {
                Console.WriteLine("{0} {1}", drCurrent["Name"].ToString(),
                                               drCurrent["Subject"].ToString());
            }

            Console.ReadKey();
        }
    }
}

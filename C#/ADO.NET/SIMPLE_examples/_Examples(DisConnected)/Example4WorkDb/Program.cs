using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Example4WorkDb
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
                                             drCurrent["Subject"].ToString(),
                                             drCurrent["Salary"].ToString());
            }


            // Формирование команды обновления cmdUpdate 
            string cmdUpdate = @"Update Persons Set SUBJECT = @SUBJECT, SALARY=@SALARY Where NAME = @NAME";
            // инициализация InsertCommand командой cmdInsert и передача параметров соединения с базой
            adapterPersons.UpdateCommand = new SqlCommand(cmdUpdate, adapterPersons.SelectCommand.Connection);

            // Формирование входных параметров при выполнении команды cmdUpdate
            SqlParameterCollection updateParameters = adapterPersons.UpdateCommand.Parameters;
            updateParameters.Add("@NAME", SqlDbType.VarChar, 10, "NAME");
            updateParameters.Add("@SUBJECT", SqlDbType.VarChar, 20, "SUBJECT");
            updateParameters.Add("@SALARY", SqlDbType.Float, 0, "SALARY");


            DataRow[] changedRows = dtPersons.Select("NAME = 'Alex'");

            foreach (var singleRow in changedRows)
            {
                singleRow["SUBJECT"] = "C#";
            }

            Console.WriteLine("Выполнение обновления! ");

            // Обновление таблицы в базе данных
            adapterPersons.Update(dtPersons);


            dtPersons.Clear();
            adapterPersons.Fill(dtPersons);

            // вывод полученных значений
            foreach (DataRow drCurrent in dtPersons.Rows)
            {
                Console.WriteLine("{0} {1}", drCurrent["Name"].ToString(),
                                             drCurrent["Subject"].ToString(),
                                             drCurrent["Salary"].ToString());
            }

            Console.ReadKey();

        }
    }
}

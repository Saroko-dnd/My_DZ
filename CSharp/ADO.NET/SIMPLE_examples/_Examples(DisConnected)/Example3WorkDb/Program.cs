using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Example3WorkDb
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


            // Формирование команды удаление cmdDelete 
            string cmdDelete = @"Delete from Persons where Id = @Id";
            // инициализация DeleteCommand командой cmdDelete и передача параметров соединения с базой
            adapterPersons.DeleteCommand = new SqlCommand(cmdDelete, adapterPersons.SelectCommand.Connection);

            // Формирование входных параметров при выполнении команды cmdDelete
            SqlParameterCollection deleteParameters = adapterPersons.DeleteCommand.Parameters;
            deleteParameters.Add("@Id",            // имя параметра 
                                 SqlDbType.Int,    // тип данных в источнике 
                                 0,               // длина столбца
                                 "Id");            // имя столбца в источнике данных  

            // вывод полученных значений
            foreach (DataRow drCurrent in dtPersons.Rows)
            {
                Console.WriteLine("{0} {1}", drCurrent["Name"].ToString(),
                                             drCurrent["Subject"].ToString());
            }

            Console.Write("Введите Id удаляемой записи: ");
            int Id = Int32.Parse(Console.ReadLine());
            DataRow[] rowsToDelete = dtPersons.Select(String.Format("Id = {0}", Id));
            Console.Write("\n");

            foreach (var row in rowsToDelete)
            {
                row.Delete();
            }

            // применение изменений
            adapterPersons.Update(dtPersons);
                       
            Console.WriteLine("Выполнение обновления! ");


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

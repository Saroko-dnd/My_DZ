using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example7CommandBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            // создание строки подключения
            string connectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";

            string commandString = "SELECT * FROM Persons";
                     
            // создания адптера для получения/обновления данных
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);

            // создание SqlCommandBuilder! 
            // Класс автоматически генерирует однотабличные команды, 
            // которые позволяют согласовать изменения, вносимые в объект DataSet, 
            // со связанной базой данных SQL Server.
            
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

            DataTable persons = new DataTable("Persons");
            adapter.Fill(persons);

            //ДОБАВЛЕНИЕ НОВЫХ ДАННЫХ В DataTable

            persons.LoadDataRow(new object[] { 100, "Olga", "WPF", 777 }, false);

            adapter.Update(persons);

            persons.Clear();
            adapter.Fill(persons);

            Console.WriteLine("Пользователи после добавления...");

            foreach (DataRow row in persons.Rows)
            {
                foreach (DataColumn column in persons.Columns)
                    Console.WriteLine("{0}: {1}", column.ColumnName, row[column]);

                Console.WriteLine();
            }
            Console.ReadKey();
            Console.Clear();


            //ИЗМЕНЕНИЕ 

            DataRow[] changedRows = persons.Select("Name = 'Olga'");

            foreach (DataRow singleRow in changedRows)
            {
                singleRow[1] = "Inna";
                singleRow[2] = "WinFrms";
                singleRow[3] = 555;
            }

            adapter.Update(persons);

            persons.Clear();
            adapter.Fill(persons);

            Console.WriteLine("Пользователи после обновления...");

            foreach (DataRow row in persons.Rows)
            {
                foreach (DataColumn column in persons.Columns)
                    Console.WriteLine("{0}: {1}", column.ColumnName, row[column]);

                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();


            //*********************************************Deleting Data******************************************
            DataRow[] rowsToDelete = persons.Select("Name = 'Inna'");

            foreach (DataRow singleRow in rowsToDelete)
            {
                singleRow.Delete();
            }

            adapter.Update(persons);

            persons.Clear();
            adapter.Fill(persons);

            Console.WriteLine("Пользователи после удаления...");

            foreach (DataRow row in persons.Rows)
            {
                foreach (DataColumn column in persons.Columns)
                    Console.WriteLine("{0}: {1}", column.ColumnName, row[column]);

                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
        }


    }
}

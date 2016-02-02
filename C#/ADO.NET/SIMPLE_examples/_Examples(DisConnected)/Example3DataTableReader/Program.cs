using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example3DataTableReader
{
    // использование метода GetSchemaTable для получения информации о схеме таблицы к которой обращается объект DataReader

    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Persons", connection);

            SqlDataReader reader = cmd.ExecuteReader();

            //получение информации о схеме таблицы Customers
            DataTable schemaTable = reader.GetSchemaTable();

            // Вывод на экран информации, предоставляемой методом GetSchemaTable()
            // Дательная информация о каждом столбце 
            foreach (DataRow row in schemaTable.Rows) 
            {
                foreach (DataColumn column in schemaTable.Columns)
                    Console.WriteLine("{0}: {1}", column.ColumnName, row[column]);

                Console.WriteLine();
            }


            DataTable customers = new DataTable("Persons");
            // Формирование столбцов таблицы Customers на основе схемы
            foreach (DataRow row in schemaTable.Rows)
            {
                var dataColumnToInsert = new DataColumn((string)row["ColumnName"], (Type)row["DataType"]);
                // Добавление столбцов в таблицу customers
                customers.Columns.Add(dataColumnToInsert); 
            }

            Console.WriteLine(new string('-', 20));

            // вывод имен и типов данных столбцов таблицы Customers
            foreach (DataColumn customersColumn in customers.Columns)
                Console.WriteLine("{0}: {1}", customersColumn.ColumnName, customersColumn.DataType);


            WriteDataFromReader(customers, reader);
            Console.WriteLine();

            foreach (DataRow row in customers.Rows)
            {
                Console.WriteLine("{0}: {1} {2} {3}", row[0], row[1], row[2], row[3]);
            }


            reader.Close();
            connection.Close();

            Console.ReadLine();
      
        }


        // Метод заполнения DataTable данными из SqlDataReader
        private static void WriteDataFromReader(DataTable table, SqlDataReader reader)
        {
            while (reader.Read())
            {
                DataRow row = table.NewRow();

                for (int i = 0; i < reader.FieldCount; i++)
                    row[i] = reader[i];
                // Добавление в таблицу новой строки
                table.Rows.Add(row);
            }
        }

    }
}

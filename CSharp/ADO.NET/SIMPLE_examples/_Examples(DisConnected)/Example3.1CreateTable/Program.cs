using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example3._1CreateTable
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Persons", connection);

            SqlDataReader reader = cmd.ExecuteReader();

            // Создание новой таблицы на основе схемы, предоставляемой DataReader
            DataTable table = CreateSchemaFromReader(reader, "Persons"); // вариант "в ручную"

        
            // Вывод структуры таблицы (имя столбца - тип столбца)
            foreach (DataColumn column in table.Columns)
                Console.WriteLine("{0}: {1}", column.ColumnName, column.DataType);

            // Запись данных  в таблицу с помощью DataReader
            WriteDataFromReader(table, reader); 
            Console.WriteLine();

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                    Console.WriteLine("{0}: {1}", column.ColumnName, row[column]);

                Console.WriteLine();
            }

            reader.Close();
            connection.Close();

            Console.ReadKey();
        }

        // Метод формирования DataTable с именем tableName из SqlDataReader
        private static DataTable CreateSchemaFromReader(SqlDataReader reader, string tableName)
        {
            DataTable table = new DataTable(tableName);
            // Добавление в таблицу нового столбца (Имя - Тип )
            for (int i = 0; i < reader.FieldCount; i++)
                table.Columns.Add(new DataColumn(reader.GetName(i), reader.GetFieldType(i)));

            return table;
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Example1WorkDb
{
    class Program
    {
        // Пример чтения данных в автономном режиме!

        static void Main(string[] args)
        {
            SqlConnection sqlConn = new SqlConnection();
            try
            {
                // СОЗДАНИЕ ПОДКЛЮЧЕНИЯ
                string sConnectionString = @"Data Source=(localdb)\v11.0;AttachDbFilename=D:\KIN\DB\People.mdf;Integrated Security=True";
                sqlConn.ConnectionString = sConnectionString;
                sqlConn.Open();


                // Создание адаптера для связи с автономной частью!
                // 1-йы способ
                SqlDataAdapter daSelectPersons1 = new SqlDataAdapter();
                daSelectPersons1.SelectCommand = new SqlCommand(@"Select * From Persons", sqlConn);

                // 2-ой способ
                SqlDataAdapter daSelectPersons2 = new SqlDataAdapter(@"Select * From Persons", sqlConn);

                // ЗАКРЫТИЕ СОЕДИНЕНИЯ
                sqlConn.Close();
               
                // 3-ый способ
                SqlCommand selCommand=new SqlCommand(@"Select * From Persons", sqlConn);
                SqlDataAdapter daSelectPersons3 = new SqlDataAdapter(selCommand);


                 // 4-ий способ
                SqlDataAdapter daSelectPersons4 = new SqlDataAdapter(@"Select * From Persons", sConnectionString);


                // РАБОТАЕМ В АВТОНОМНОМ РЕЖИМЕ!

                // ПОЛУЧЕНИЕ ДАННЫХ
                           

                // 1-ый способ (заполнение непосресдвтенно DataTable)
                // Создание таблицы  dtPersons
                DataTable dtPersons1 = new DataTable("Persons");
                daSelectPersons4.Fill(dtPersons1);

                // 2-ой способ (создание и заполнение таблицы "Persons" в DataSet)
                DataSet dsPersons = new DataSet();
                daSelectPersons4.Fill(dsPersons, "Persons");
                DataTable dtPersons2 = dsPersons.Tables["Persons"];   // получение таблицы по имени из DataSet

                // Вывод на экран содержимого DataTable ("Persons")
                // для этого формируем объект строки и обрашаемся 
                // к столбцам по имени drCurrent["Name"], drCurrent["Subject"]
                foreach (DataRow drCurrent in dtPersons2.Rows)
                {
                    Console.WriteLine("{0} {1}", drCurrent["Name"].ToString(),
                                                    drCurrent["Subject"].ToString());
                }

                Console.ReadLine();
            }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }
             finally
             {
                 if (sqlConn.State==ConnectionState.Open)
                     sqlConn.Close();
             }

             Console.ReadKey();
        }
    }
}

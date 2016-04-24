using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example5
{
    class Program
    {
        static void Main(string[] args)
        {
        
            // СОЗДАНИЕ ПОДКЛЮЧЕНИЯ
            string sConnectionString;
            sConnectionString = @"Data Source=A1-PREPOD;Initial Catalog=People;Integrated Security=True;";
            SqlConnection sqlConn = new SqlConnection(sConnectionString);
            sqlConn.Open();

            // Создание адаптера для связи с автономной частью!
            // 1-йы способ
            SqlDataAdapter daSelectPersons1 = new SqlDataAdapter();
            daSelectPersons1.SelectCommand = new SqlCommand(@"Select * From Persons", sqlConn);

            // 2-ой способ
            SqlDataAdapter daSelectPersons2 = new SqlDataAdapter(@"Select * From Persons", sqlConn);

            // ЗАКРЫТИЕ СОЕДИНЕНИЯ
            sqlConn.Close();  

            // 3-ий способ
            SqlDataAdapter daSelectPersons3 = new SqlDataAdapter (@"Select * From Persons", sConnectionString);


            // РАБОТАЕМ В АВТОНОМНОМ РЕЖИМЕ!

            // ПОЛУЧЕНИЕ ДАННЫХ
            // Создание таблицы  dtPersons
            DataTable dtPersons1 = new DataTable("Persons");

            // Получение данных 

            // 1-ый способ (заполнение непосресдвтенно DataTable)
            daSelectPersons3.Fill(dtPersons1);

            // 2-ой способ (создание и заполнение таблицы "Persons" в DataSet)
            DataSet dsPersons = new DataSet();
            daSelectPersons3.Fill(dsPersons,"Persons");
            DataTable dtPersons2 = dsPersons.Tables["Persons"]; // получение таблицы по имени из DataSet
          
            // Вывод на экран содержимого DataTable ("Persons")
            // для этого формируем объект строки и обрашаемся 
            // к столбцам по имени drCurrent["Name"], drCurrent["Subject"]
            foreach (DataRow drCurrent in dtPersons2.Rows)
            {
            Console.WriteLine("{0} {1}",   drCurrent["Name"].ToString(),
                                            drCurrent["Subject"].ToString());
            }
         
            Console.ReadLine();
        }
    }
}

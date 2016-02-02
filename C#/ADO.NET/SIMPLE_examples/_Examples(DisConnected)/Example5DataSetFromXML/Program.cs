using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example5DataSetFromXML
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание DataSet с именем AutoPark
            DataSet dataSet = new DataSet("AutoPark");
            // Формирование заполнение dataSet из  XML-файла
            dataSet.ReadXml("AutoPark.xml");
            dataSet.ReadXmlSchema("AutoParkSchema.xsd");

            Console.WriteLine("XML успешного загружены");

            // Вывод названия таблиц
            Console.WriteLine("*** Названия таблиц  ***");
            Console.WriteLine(dataSet.Tables[0].TableName);
            Console.WriteLine(dataSet.Tables[1].TableName);

            // Вывод количества строк в таблицах 
            Console.WriteLine("*** Количество строк в таблицах  ***");
            Console.WriteLine(dataSet.Tables[0].Rows.Count);
            Console.WriteLine(dataSet.Tables[1].Rows.Count);
            
            // Вывод данных из таблицы Cars
            Console.WriteLine("*** Данные из таблиц  ***");
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Console.WriteLine("{0}, {1}", row["Name"], row["Country"]);
            }

           
            Console.WriteLine(new string('-', 40));

            // Вывод данных из таблицы Drivers
            foreach (DataRow row in dataSet.Tables[1].Rows)
            {
                Console.WriteLine("{0}, {1}", row["Name"], row["Age"]);
            }

            Console.ReadKey();

        }
    }
}

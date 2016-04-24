using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example3DataSetToXML
{
    class Program
    {
        static void Main(string[] args)
        {
     // Создание Таблицы DataTable с именем "Cars"
            DataTable tablCars = new DataTable("Cars");
            // Создание столбцов
            DataColumn carsId = new DataColumn("Id", typeof(int));
            DataColumn carsName = new DataColumn("Name", typeof(string));
            DataColumn carsCountry = new DataColumn("Country", typeof(string));
            DataColumn carsPrice = new DataColumn("Price", typeof(double));
            tablCars.Columns.AddRange(new DataColumn[] { carsId, carsName, carsCountry, carsPrice });
            // Создание строки с данными 
            DataRow newRow1 = tablCars.NewRow();
            newRow1["Name"] = "BMW"; newRow1["Country"] = "Germany";
            newRow1["Price"] = "50000";
            tablCars.Rows.Add(newRow1);

            DataRow newRow2 = tablCars.NewRow();
            newRow2["Name"] = "Audi"; newRow2["Country"] = "Germany";
            newRow2["Price"] = "37500";
            tablCars.Rows.Add(newRow2);

            // Сохранить ТАБЛИЦЫ  tablCars  в виде XML
            tablCars.WriteXml("Cars.xml");
            tablCars.WriteXmlSchema("CarsSchema.xsd");
         

      // Создание Таблицы DataTable с именем "Drivers"
            DataTable tablDrivers = new DataTable("Drivers");
            // Создание столбцов
            DataColumn drId = new DataColumn("Id", typeof(int));
            DataColumn drName = new DataColumn("Name", typeof(string));
            DataColumn drAge = new DataColumn("Age", typeof(string));
            tablDrivers.Columns.AddRange(new DataColumn[] { drId, drName, drAge });
            // Создание строки с данными 
            DataRow newRow3 = tablDrivers.NewRow();
            newRow3["Name"] = "Ivan"; newRow3["Age"] = "33";
            tablDrivers.Rows.Add(newRow3);

            DataSet dataSet = new DataSet("AutoPark");
            dataSet.Tables.AddRange(new DataTable[] { tablCars, tablDrivers});


            // Сохранить DATASET  в виде XML
            dataSet.WriteXmlSchema("AutoParkSchema.xsd");
            dataSet.WriteXml("AutoPark.xml");
            
            
            // Очистить DataSet.
            dataSet.Clear();

            Console.WriteLine("XML успешно сформированы!");

            Console.ReadKey();

        }
    }
}

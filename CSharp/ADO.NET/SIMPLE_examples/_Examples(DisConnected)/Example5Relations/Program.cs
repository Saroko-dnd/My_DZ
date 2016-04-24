using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example5Relations
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet ds = new DataSet();
           
            DataTable tablDrivers = CreateTableDrivers(); 

            DataTable tablCars = CreateTableCar();

            ds.Tables.AddRange(new[] { tablDrivers, tablCars });

            // создание отношения между таблицами Drivers и Cars
            DataRelation relation = new DataRelation("FK_DriversCars", // имя отношения 
                                         tablDrivers.Columns["Id"],    // поле родительской таблицы
                                         tablCars.Columns["IdDriver"], // поле дочерней таблицы
                                         false);                        // создавать/не создавать ограничения
            
            // после созания ограничения его нужно добавить в коллекцию Relations 
            // объекта DataSet, в которой содержаться таблицы
            // без этого шага отношение не будет работать
            ds.Relations.Add(relation);



            Console.WriteLine("Вывод информации через дочерние строки ");
            foreach (DataRow driverRow in tablDrivers.Rows)
            {
                Console.WriteLine(" Владелец: {0}", driverRow["Name"]);
                Console.WriteLine(" Автомобили: ");
                // метод GetChaildRows получает дочерние строки в виде массива DataRow[]
                foreach (DataRow carsRow in driverRow.GetChildRows(relation))
                {
                    Console.WriteLine("{0} {1}", carsRow["Name"], carsRow["Price"]);
                }
            }



            Console.WriteLine("Вывод информации через родительские строки ");
            foreach (DataRow carsRow in tablCars.Rows)
            {
                Console.Write(" Атомобиль: {0} {1}", carsRow["Name"], carsRow["Price"]);
                Console.Write(" Владелец: ");
                // метод GetParrentRow возвращает родительские строки в виде массива DataRow[]
                foreach (DataRow driverRow in carsRow.GetParentRows(relation))
                {
                    Console.WriteLine("{0}", driverRow["Name"]);
                }
            }
           

            ds.WriteXmlSchema("Registration.xsd");
            ds.WriteXml("Registration.xml");

            Console.ReadKey();
        }

        private static DataTable CreateTableDrivers()
        {
           
            DataTable tablDrivers = new DataTable("Drivers");
           
            DataColumn drId = new DataColumn("Id", typeof(int));
            drId.AutoIncrement = true;
            drId.AutoIncrementSeed = 1;  drId.AutoIncrementStep = 1;
            drId.Unique = true;
            drId.AllowDBNull = true;
            DataColumn drName = new DataColumn("Name", typeof(string));
            DataColumn drAge = new DataColumn("Age", typeof(string));
           // DataColumn drCarId = new DataColumn("IdCar", typeof(int));
            tablDrivers.Columns.AddRange(new DataColumn[] { drId, drName, drAge });


            // установка первичного ключа 
            tablDrivers.PrimaryKey = new DataColumn[] { tablDrivers.Columns["Id"] };
                       
            DataRow newRow1 = tablDrivers.NewRow();
            newRow1["Name"] = "Ivan"; newRow1["Age"] = "33"; //newRow1["drCarId"] = 10;
            tablDrivers.Rows.Add(newRow1);
            DataRow newRow2 = tablDrivers.NewRow();
            newRow2["Name"] = "Alex"; newRow2["Age"] = "28"; // newRow2["drCarId"] = 11;
            tablDrivers.Rows.Add(newRow2);
            DataRow newRow3 = tablDrivers.NewRow();
            newRow3["Name"] = "Inna"; newRow3["Age"] = "18"; //newRow3["drCarId"] = 12;
            tablDrivers.Rows.Add(newRow3);

            return tablDrivers;
        }

        private static DataTable CreateTableCar()
        {
            // Создание Таблицы DataTable с именем "Cars"
            DataTable tablCars = new DataTable("Cars");
            // Создание столбцов
            DataColumn carsId = new DataColumn("Id", typeof(int));
            carsId.AutoIncrement = true;
            carsId.AutoIncrementSeed = 1; carsId.AutoIncrementStep = 1;
            DataColumn carsName = new DataColumn("Name", typeof(string));
            DataColumn carsCountry = new DataColumn("Country", typeof(string));
            DataColumn carsPrice = new DataColumn("Price", typeof(double));
            DataColumn carsDriverId = new DataColumn("IdDriver", typeof(int));
            tablCars.Columns.AddRange(new DataColumn[] { carsId, carsName, carsCountry, carsPrice, carsDriverId });

            tablCars.PrimaryKey = new DataColumn[] { tablCars.Columns["Id"] };
            
            // Создание строки с данными 
            DataRow newRow1 = tablCars.NewRow();
            newRow1["Name"] = "BMW"; newRow1["Country"] = "Germany";
            newRow1["Price"] = "50000"; newRow1["IdDriver"] = 1;
            tablCars.Rows.Add(newRow1);

            DataRow newRow2 = tablCars.NewRow();
            newRow2["Name"] = "Audi"; newRow2["Country"] = "Germany";
            newRow2["Price"] = "37500"; newRow2["IdDriver"] = 2;
            tablCars.Rows.Add(newRow2);

            DataRow newRow3 = tablCars.NewRow();
            newRow3["Name"] = "Opel"; newRow3["Country"] = "Germany";
            newRow3["Price"] = "19500"; newRow3["IdDriver"] = 2;
            tablCars.Rows.Add(newRow3);

            DataRow newRow4 = tablCars.NewRow();
            newRow4["Name"] = "Ford"; newRow4["Country"] = "USA";
            newRow4["Price"] = "49500"; newRow4["IdDriver"] = 3;
            tablCars.Rows.Add(newRow4);

            return tablCars;
        }
    }
}

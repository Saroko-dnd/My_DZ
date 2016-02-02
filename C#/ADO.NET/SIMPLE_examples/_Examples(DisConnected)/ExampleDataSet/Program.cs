using System;
using System.Text;

using System.Data;

namespace ExampleDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание DataSet с именем "AutoPark"
            DataSet ds = new DataSet("AutoPark");
            ds.ExtendedProperties["DateCreation"] = DateTime.Now;
            ds.ExtendedProperties["Name"] = "BMW Corp.";

            // Создание Таблицы DataTable с именем "Cars"
            DataTable tablCars = new DataTable("Cars");

            // Создание Столбца DataColumn
            DataColumn colId = new DataColumn();
            colId.ColumnName = "Id";                // имя столбца
            colId.DataType=typeof(int);             // тип элементов столбца
            colId.AutoIncrement = true;             // поддержка автоинкримента (автомат. увеличение значения столбца)
            colId.AutoIncrementSeed = 1;                // начальное значение
            colId.AutoIncrementStep = 1;                // шаг изменения
            colId.ReadOnly = true;                  // только чтение
            colId.AllowDBNull = false;              // может содержать значения NULL
            colId.Unique = true;                    // уникальное поле
            colId.Caption = "№";                    // наименование столбца (дружественное имя)
            tablCars.Columns.Add(colId);     // Добавление столбца в таблицу

            // установка первичного ключа (расм. далее)
            tablCars.PrimaryKey = new DataColumn[] { tablCars.Columns["Id"] };


            // Создание Столбца DataColumn
            DataColumn colName = new DataColumn("Name", typeof(string));
            colName.AllowDBNull = false;
            colId.Caption = "Марка";
            tablCars.Columns.Add(colName);     // Добавление столбца в таблицу

            DataColumn colCountry = new DataColumn("Country", typeof(string));
            colCountry.Caption = "Страна производитель";
            colCountry.ColumnMapping = MappingType.Element; // тип столбца при сохранениии в XML - Элемент

            DataColumn colPrice = new DataColumn("Price", typeof(double));
            colPrice.Caption = "Цена";
            colPrice.DefaultValue=25000;           // установка значения по умолчанию

            tablCars.Columns.AddRange(new DataColumn[] { colCountry, colPrice });   // Добавление столбцов в таблицу


            // ВЫВОД НА ЭКРАН ИМЕНИ СТОЛБЦА И ТИП СТОЛБЦА
            foreach (DataColumn column in tablCars.Columns)
                Console.WriteLine("{0}: {1};", column.ColumnName, column.DataType);

            Console.WriteLine(new string('-', 20));

            // Создание строки с данными 
            DataRow newRow1 = tablCars.NewRow();
            newRow1["Name"] = "BMW";
            newRow1["Country"] = "Germany";
            newRow1["Price"] = "50000";
            tablCars.Rows.Add(newRow1);

            DataRow newRow2 = tablCars.NewRow();
            newRow2[1] = "Audi";
            newRow2[2] = "Germany";
            tablCars.Rows.Add(newRow2);


            // ВЫВОД НА ЭКРАН ИМЕНИ СТОЛБЦА И ЗНАЧЕНИЯ ИЗ ТАБЛИЦЫ Cars
            foreach (DataRow row in tablCars.Rows)
            {
                foreach (DataColumn column in tablCars.Columns)
                    Console.WriteLine("{0}: {1}", column.ColumnName, row[column]);
            }

            Console.WriteLine(new string('-', 20));

            Console.ReadKey();
        }
    }
}

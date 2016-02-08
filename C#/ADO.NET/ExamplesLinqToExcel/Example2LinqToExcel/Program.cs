using LinqToExcel;
using LinqToExcel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example2LinqToExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            // Получение информации из Excel документа с именем SampleData.xls
            string pathFile = "SampleData.xls";

            // Имя закладки 
            string sheetName = "Orders";

            var excelFile = new ExcelQueryFactory(pathFile);
            List<OrderInfo> infoOrders = (from a in excelFile.Worksheet<OrderInfo>(sheetName)
                                         select a).Take(5).ToList();

            foreach (OrderInfo item in infoOrders)
            {
                string pattern = "Заказчик: {0,-10}   Заказ: {1,-10}  ИТОГО: {2,-5}";
                // Вывод информации из столбцов OrderDate и Total
                Console.WriteLine(string.Format(pattern, item.Customer, item.Item, item.Total));
            }


            Console.WriteLine("Вывод информации о заказах, сумма которых >500 $");

            var infoOrdersByTotal = from a in excelFile.Worksheet<OrderInfo>(sheetName) 
                                     where a.Total>500
                                     select a;

            foreach (var item in infoOrdersByTotal)
            {
                string pattern = "Заказчик: {0,-10}   Заказ: {1,-10}  ИТОГО: {2,-5}";
                // Вывод информации из столбцов OrderDate и Total
                Console.WriteLine(string.Format(pattern, item.Customer, item.Item, item.Total));
            }


            Console.WriteLine("Вывод информации о заказах, цена за шт. у которых >15 $");

            //  устанавливаем свойство "UnitCost"в сооветствие столбцу "Unit Cost"
            //  второй способ: установить для свойства аттрибут  [ExcelColumn("Unit Cost")]
            excelFile.AddMapping("UnitCost", "Unit Cost");      

            var infoOrdersByPrice = from a in excelFile.Worksheet<OrderInfo>(sheetName)
                                    where a.UnitCost > 15
                                    select a;

            foreach (var item in infoOrdersByPrice)
            {
                string pattern = "Заказчик: {0,-10}   Заказ: {1,-10}  ИТОГО: {2,-5}";
                // Вывод информации из столбцов OrderDate и Total
                Console.WriteLine(string.Format(pattern, item.Customer, item.Item, item.UnitCost));
            }

            Console.WriteLine("Вывод информации о заказчиках и регионах");

            var infoOrdersByRegion = from a in excelFile.Worksheet(sheetName)
                                     where a["Region"] == "Central" && a["Item"] == "Pencil"
                                     where a[1] == "Central" && a[3] == "Pencil"
                                     select a;

            foreach (var item in infoOrdersByRegion)
            {
                string pattern = "Заказчик: {0,-10}   Регион: {1,-10}";
                // Вывод информации из столбцов OrderDate и Total
                Console.WriteLine(string.Format(pattern, item["Customer"], item["Region"]));
            }


            Console.WriteLine("Вывод информации о заказчиках и количестве заказанного ");

            var infoOrdersByUnits = from a in excelFile.Worksheet(sheetName)
                                     where a["Units"].Cast<int>() < 10
                                     select a;

            foreach (var item in infoOrdersByUnits)
            {
                string pattern = "Заказчик: {0,-10}   Количество: {1,-10}";
                // Вывод информации из столбцов OrderDate и Total
                Console.WriteLine(string.Format(pattern, item["Customer"], item["Units"]));
            }



            Console.ReadKey();
        }
    }

    public class OrderInfo
    {
        public DateTime OrderDate { get; set; }
        public string Region { get; set; }
        public string Customer { get; set; }
        public string Item { get; set; }
        public string Units { get; set; }
       
        [ExcelColumn("Unit Cost")]
        public decimal UnitCost { get; set; }
        public decimal Total { get; set; }
    }

}

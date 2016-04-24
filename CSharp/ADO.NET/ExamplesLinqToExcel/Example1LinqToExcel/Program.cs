
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Подключение пространства имен
using LinqToExcel;

namespace Example1LinqToExcel
{

    // ВАЖНО: При использовании Windows x64 добаления сборки LinqToExcel необходимо:
    // Перйти в окно менеджера пакетов: View-Other Windows- Package Manager Console
    // ввести Install-Package LinqToExcel_x64
    // При использовании Windows x86 используем Manage NuGet Packages

    class Program
    {
        static void Main(string[] args)
        {
            // Получение информации из Excel документа с именем SampleData.xls
            string pathFile = "SampleData.xls";

            // Имя закладки 
            string sheetName = "Orders";

            var excelFile = new ExcelQueryFactory(pathFile);
            var infoOrders = from a in excelFile.Worksheet(sheetName) 
                             select a;

            foreach (var item in infoOrders)
            {
                string pattern = "Дата заказа: {0};     Итого: {1}";
               
                // Вывод информации из столбцов OrderDate и Total
                Console.WriteLine(string.Format(pattern, item["OrderDate"], item["Total"]));
            }

            Console.ReadKey();
        }
    }
}

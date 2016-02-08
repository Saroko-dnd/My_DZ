
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Подключение пространства имен
using LinqToExcel;

namespace Example1WorkExcel
{
    class Program
    {
        static void Main(string[] args)
        {

            // Получение информации из Excel документа с именем SampleData.xls
            string pathFile = "SampleData.xls";
                      
            var excelFile = new ExcelQueryFactory(pathFile);

            // Вывод списка листов (Worksheet) в Excel документе  
            foreach( var item in excelFile.GetWorksheetNames())
            {
                Console.Write(item+" ");
            }
            Console.Write("\n");

            foreach (var item in excelFile.GetWorksheetNames())
            {
                Console.Write(item + ":");
                foreach (var itemRow in excelFile.GetColumnNames(item))
                {
                    Console.Write(itemRow + " ");
                }
                Console.Write("\n");
            }


            // установка документа "только для чтения"
            excelFile.ReadOnly = true;

            Console.ReadKey();
        }
    }
}

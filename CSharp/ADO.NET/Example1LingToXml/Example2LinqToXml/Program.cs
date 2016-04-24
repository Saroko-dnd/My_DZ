using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Example2LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {

            // установка разделитя в дробных суммах (зависит от настройки локализации)
            NumberFormatInfo formatSepar = new NumberFormatInfo();
            formatSepar.NumberDecimalSeparator = ".";


            // Загрузка документа в память
            XDocument xmlDoc = XDocument.Load("books.xml");


            IEnumerable<XElement> books = xmlDoc.Root.Elements("book")
                                           .Where(t => t.Element("genre").Value == "Computer");

            // XDocument.Descendants(XName)
            // Возвращает коллекцию подчиненных узлов для данного элемента.
            // Только элементы, имеющие соответствующее XName, включаются в коллекцию. 
            
            //IEnumerable<XElement> books = xmlDoc.Root.Descendants("book")
            //                              .Where(t => t.Element("genre").Value == "Computer");
           
            books.Remove();

            xmlDoc.Save("booksNew.xml");

            Console.WriteLine("Удаление выполнено успешно...");


            Console.ReadKey();
        }
    }
}

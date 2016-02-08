using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Example3LinqToXml
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

            var albumGroups = xmlDoc.Root.Elements("book").GroupBy(t => t.Element("genre").Value);
            foreach (IGrouping<string, XElement> a in albumGroups)
                Console.WriteLine("{0} - {1}", a.Key, a.Count());

            Console.ReadKey();
        }
    }
}

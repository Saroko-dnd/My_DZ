using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Example3UpdateXml
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberFormatInfo formatSepar = new NumberFormatInfo();
            formatSepar.NumberDecimalSeparator = ".";

            XDocument doc = XDocument.Load("books.xml");
            foreach (XElement el in doc.Root.Elements("book"))
            {
                decimal price = Decimal.Parse(el.Element("price").Value, formatSepar);
                el.SetElementValue("price", price+100);
            }
            doc.Save("booksNew.xml");

            Console.WriteLine("Обновление выполнено успешно...");

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// подключение пространства имен
using System.Xml.Linq;

namespace Example1LingToXml
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


            Console.WriteLine("формирование запроса для получения книг, цена которых больше 15 $");
            IEnumerable<XElement> booksPrice =
                          from t in xmlDoc.Root.Elements("book")
                          where Decimal.Parse(t.Element("price").Value, formatSepar) > 15
                          select t;

            foreach (XElement xmlItem in booksPrice)
            {
                Console.WriteLine(xmlItem.Element("author").Value+ " price: "+xmlItem.Element("price").Value+"$");
            }


            Console.WriteLine("Запрос для получения книг, фамилия автора у которых начинается на букву \"К\"");
            IEnumerable<XElement> booksAuthor =
                         from t in xmlDoc.Root.Elements("book")
                         where t.Element("author").Value.StartsWith("K")
                         orderby Decimal.Parse(t.Element("price").Value, formatSepar)
                         select t;

            foreach (XElement xmlItem in booksAuthor)
            {
                 Console.WriteLine(xmlItem.Element("author").Value+ " price: "+xmlItem.Element("price").Value+"$");
            }




            Console.ReadKey();
        }
    }
}

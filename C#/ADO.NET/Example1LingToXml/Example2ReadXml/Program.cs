using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Example2ReadXml
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("books.xml");
            foreach (XElement el in doc.Root.Elements())
            {
                //Выводим имя элемента и значение аттрибута id
                Console.WriteLine("{0} {1}", el.Name, el.Attribute("id").Value);
                Console.WriteLine("  Attributes:");
                //выводим в цикле все аттрибуты, заодно смотрим как они себя преобразуют в строку
                foreach (XAttribute attr in el.Attributes())
                    Console.WriteLine("    {0}", attr);
                Console.WriteLine("  Elements:");
                //выводим в цикле названия всех дочерних элементов и их значения
                foreach (XElement element in el.Elements())
                    Console.WriteLine("    {0}: {1}", element.Name, element.Value);
            }

            Console.ReadKey();
        }
    }
}

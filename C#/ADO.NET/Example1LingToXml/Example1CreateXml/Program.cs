using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Example1CreateXml
{
    class Program
    {
        static void Main(string[] args)
        {

            XDocument doc = new XDocument();
            XElement catalog = new XElement("catalog");
            doc.Add(catalog);

            //создаем элемент "track"
            XElement book = new XElement("book");
            
            //добавляем необходимые атрибуты
            book.Add(new XAttribute("id", "bk101"));

            //создаем элемент "author"
            XElement author = new XElement("author");
            author.Value = "Gambardella, Matthew";
            book.Add(author);

            //создаем элемент "title"
            XElement title = new XElement("title");
            title.Value = "XML Developer's Guide";
            book.Add(title);

            //создаем элемент "genre"
            XElement genre = new XElement("genre");
            genre.Value = "Computer";
            book.Add(genre);


            //создаем элемент "price"
            XElement price = new XElement("price");
            price.Value = "44.95";
            book.Add(price);

            //создаем элемент "publish_date"
            XElement publish_date = new XElement("publish_date");
            publish_date.Value = "2000-10-01";
            book.Add(publish_date);

            //создаем элемент "description"
            XElement description = new XElement("description");
            description.Value = "An in-depth look at creating applications with XML.";
            book.Add(description);

            doc.Root.Add(book);


            //сохраняем наш документ
            doc.Save("books.xml");
        }
    }
}

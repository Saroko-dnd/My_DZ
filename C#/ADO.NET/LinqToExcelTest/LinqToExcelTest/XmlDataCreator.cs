using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace LinqToExcelTest
{
    public static class XmlDataCreator
    {
        public static XDocument XmlDataObject = new XDocument();
        public static void CreateAndSaveXML()
        {
            XElement RootFirstElement = new XElement("Orders");
            XElement RootSecondElement = new XElement("Prices");
            XElement ExcelMainRoot = new XElement("Excel");
            RootFirstElement.Add(new XElement("Продукт"));
            RootFirstElement.Add(new XElement("скидка"));
            RootSecondElement.Add(new XElement("Производитель"));
            RootSecondElement.Add(new XElement("Цена"));
            ExcelMainRoot.Add(RootFirstElement);
            ExcelMainRoot.Add(RootSecondElement);
            XmlDataObject.Add(ExcelMainRoot);
            XmlDataObject.Save("BaseSelector.xml");
        }
    }
}

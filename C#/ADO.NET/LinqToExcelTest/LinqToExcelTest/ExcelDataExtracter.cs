using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml;

namespace LinqToExcelTest
{
    public static class ExcelDataExtracter
    {
        public static ExcelQueryFactory ExcelData = new ExcelQueryFactory("PriceProductBase.xls");
        public static XDocument XmlDataSelector = XDocument.Load("BaseSelector.xml");

        public static bool InitializeExcelFactory (string PathToExcel)
        {
            bool success = false;
            try
            {
                ExcelData = new ExcelQueryFactory(PathToExcel);
                success = true;
            }
            catch (Exception CurException)
            {
                MessageBox.Show(CurException.Message, Properties.Resources.Error,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return success;
        }
    }
}

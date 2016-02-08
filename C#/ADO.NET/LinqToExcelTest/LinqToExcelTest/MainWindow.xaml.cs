using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace LinqToExcelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


           // XmlDataCreator.CreateAndSaveXML();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<StringContainer> AllElements = new List<StringContainer>();
            List<XElement> PricesElements = ExcelDataExtracter.XmlDataSelector.Root.Elements()
                .ToList();
            int counter = 0;
            foreach (XElement CurrentElement in PricesElements)
            {
                AllElements.Add(new StringContainer() { Name = CurrentElement.Name.LocalName });

                foreach (XElement SmallElement in CurrentElement.Elements())
                {
                    AllElements[AllElements.Count - 1].Elements.Add(SmallElement.Name.LocalName);
                }
            }
            //MainDataGrid.ItemsSource = from ExcelDataExtracter

            //List<XElement> FirstListOfElements = PricesElements.Elements().Where(result => result.Name in );
        }        
    }

    public class StringContainer
    {
        public string Name_; 
        
        public string Name
        {
            get
            {
                return Name_;
            }
            set
            {
                Name_ = value;
            }
        }

        public List<string> Elements = new List<string>();
    }
}

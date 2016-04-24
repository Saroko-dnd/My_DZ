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

namespace BrowserPanel
{

     // Демонстрация создания пользовательского элемента управления
     // класс UserControl  позволяет создавать пользовательские элементы
     // на основе существующих элементов 
   
    public partial class BrowsePanelControlNo : UserControl
    {
        public BrowsePanelControlNo()
        {
            InitializeComponent();
        }

        string fileName;
        public string FileName
        {
            get { return fileName;  }
            set { fileName = value; }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            if (openFileDlg.ShowDialog() == true)
            {
                tbPathFile.Text = openFileDlg.FileName;
                this.FileName = openFileDlg.FileName;
            }
        }
    }
}

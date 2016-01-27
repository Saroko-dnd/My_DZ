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
   
    public partial class BrowsePanelControl : UserControl
    {
        public BrowsePanelControl()
        {
            InitializeComponent();
        }

        // Создание свойства зивисимости
        public static readonly DependencyProperty FileNameProperty;
        static BrowsePanelControl()
        {
            // Создание метаданных с указанием 
            // 1. Значения по умолчанию
            // 2. Метода который будет выполнятся при изменении значения свойства зависимости
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("С:\\Windows", new PropertyChangedCallback(OnFileNameChanged));
            // регистрация свойства зивисимости
            FileNameProperty = DependencyProperty.Register
            ( "FileName", 
              typeof(string),
              typeof(BrowsePanelControl),
              metadata,
              new ValidateValueCallback(Validate));
        }

        // метод вызываемый при изменении свойтсва зависисмости
        private static void OnFileNameChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            // вызывается метод, который изменяет значение TextBox 
            // при изменении свойства зависимости через Binding
            ((BrowsePanelControl)element).SetValueTextBox();
        }
        void SetValueTextBox()
        {
            tbPathFile.Text = this.FileName;
        }
       
        static bool Validate(object value)
        {
            if ((value as string).Length==0)
                return false;
            return true;
        }
       
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { 
                  SetValue(FileNameProperty, value);
               }
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

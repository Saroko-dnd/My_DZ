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

namespace Example6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         public MainWindow()
        {
            InitializeComponent();

            List<Message> Messages = new List<Message>()
            {
                new Message{ID=1, Category=1, Topic="Тема 1", Content="C# - объектно-ориентированный язык программирования для платформы .NET", Author="Шилдт", GetDate=DateTime.Now},
                 new Message{ID=2,  Category=1,Topic="Тема 2", Content="WPF - библиотека, позволяющая разрабатывать приложения с красивым пользовательским интерфесом", Author="МакДональд", GetDate=DateTime.Now},
                  new Message{ID=3,  Category=2,Topic="Тема 3", Content="ASP.NET - технология, позволяющая разрабатывать web-приложения на базе платформы .NET", Author="Сандерсон", GetDate=DateTime.Now},
                   new Message{ID=4,  Category=1,Topic="Тема 4", Content="WCF - технология, позволяющая разрабатывать сервисно-ориентированные приложения", Author="Кто-то...",GetDate=DateTime.Now},
            };

            lbMessages.ItemsSource = Messages;
        }

         private void Button_Click(object sender, RoutedEventArgs e)
         {

         }

    }
}

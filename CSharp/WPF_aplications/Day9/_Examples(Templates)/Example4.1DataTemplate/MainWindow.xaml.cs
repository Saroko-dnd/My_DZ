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

namespace Example5DataTemplate
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
                new Message{ID=1, Topic="Тема 1", Content="C# - объектно-ориентированный язык программирования для платформы .NET", Author="Шилдт", GetDate=DateTime.Now},
                 new Message{ID=2, Topic="Тема 2", Content="WPF - библиотека, позволяющая разрабатывать приложения с красивым пользовательским интерфесом", Author="МакДональд", GetDate=DateTime.Now},
                  new Message{ID=3, Topic="Тема 3", Content="ASP.NET - технология, позволяющая разрабатывать web-приложения на базе платформы .NET", Author="Сандерсон", GetDate=DateTime.Now},
                   new Message{ID=4, Topic="Тема 4", Content="WCF - технология, позволяющая разрабатывать сервисно-ориентированные приложения", Author="Кто-то...",GetDate=DateTime.Now},
            };

            lbxMessages.ItemsSource = Messages;

        }

        // Получение данных из DataContext кнопки элемента listBox
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Message message = (Message)button.DataContext;
            this.Title= message.Topic; 
        }

        // Получение данных из выбранного элемента  listBox
        private void lbxMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Message selMessage=(Message)lbxMessages.SelectedItem;
            tbInfo.Text = selMessage.Topic + "\n" + selMessage.Content;
        }
    }

   
}

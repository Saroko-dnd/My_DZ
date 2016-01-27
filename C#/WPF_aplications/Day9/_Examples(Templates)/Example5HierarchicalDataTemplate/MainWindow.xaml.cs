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

namespace Example5HierarchicalDataTemplate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] Images =
        {
          "/Images/Main.png",
          "/Images/Node.png",
          "/Images/Item.png",
        };

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          List<ContentTree> content=new List<ContentTree>();
          ContentTree mainNode = new ContentTree()
          {
              Text = ".NET",
              Logo = Images[0]
          };

          mainNode.Children.Add(
              new ContentTree()
              {
                  Text = "C# ",  Logo = Images[1],
                  Children = new List<ContentTree>() 
                  {
                    new ContentTree(){Text="Классы", Logo = Images[2]},
                    new ContentTree(){Text="Инкапсуляция", Logo = Images[2]},
                    new ContentTree(){Text="Наследование", Logo = Images[2]},
                    new ContentTree(){Text="Полиморфизм", Logo = Images[2]},
                    new ContentTree(){Text="Абстракция", Logo = Images[2]},
                  }
              }
              );

           mainNode.Children.Add(
           new ContentTree()
           {
               Text = "WPF ",Logo = Images[1],
               Children = new List<ContentTree>() 
                  {
                    new ContentTree(){Text="Элементы управления", Logo = Images[2]},
                    new ContentTree(){Text="Ресурсы", Logo = Images[2]},
                    new ContentTree(){Text="Стили", Logo = Images[2]},
                    new ContentTree(){Text="Шаблоны", Logo = Images[2]},
                    new ContentTree(){Text="Привязка данных", Logo = Images[2]},
                  }
           }
           );

         mainNode.Children.Add(
         new ContentTree()
         {
             Text = "ASP.NET", Logo = Images[1],
             Children = new List<ContentTree>() 
                  {
                    new ContentTree(){Text="Web Forms", Logo = Images[2]},
                    new ContentTree(){Text="MVC", Logo = Images[2]},
                    new ContentTree(){Text="Web API", Logo = Images[2]},
                  }
         }
         );

          content.Add(mainNode);
          tvMain.ItemsSource = content;
        }
    }
}

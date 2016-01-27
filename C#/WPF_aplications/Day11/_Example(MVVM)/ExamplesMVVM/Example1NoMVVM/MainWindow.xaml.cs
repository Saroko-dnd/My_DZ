using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace NoMVVM
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Загрузка окна 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Привязка списка клиентов к ListBox
            ListBoxPersons.ItemsSource = ClientRepository.AllClients; 
        }

        // Добавление нового клиента в список клиентов
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFirstName.Text) ||  string.IsNullOrEmpty(TextBoxLastName.Text))
            {
                return;
            }
            else
            {
                Client client = new Client();
                client.FirstName = TextBoxFirstName.Text;
                client.LastName = TextBoxLastName.Text;
                ClientRepository.AllClients.Add(client);
            }
        }
    }
}

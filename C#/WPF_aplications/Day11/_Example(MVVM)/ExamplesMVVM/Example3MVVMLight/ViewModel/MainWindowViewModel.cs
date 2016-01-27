using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVMLight_Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMLight_Sample.ViewModel
{
    // ViewModelBase -  базовый класс для виью моделей из пространства имен GalaSoft.MvvmLight;
    public class MainWindowViewModel : ViewModelBase
    {
        Client _currentClient;
        public Client CurrentClient
        {
            get
            {
                if (_currentClient == null)
                    _currentClient = new Client();
                return _currentClient;
            }
            set
            {
                _currentClient = value;
                // Метод из базового класса для запуска события NotifyPropertyChanged
                RaisePropertyChanged("CurrentClient");
            }
        }

        ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                if (_clients == null)
                    _clients = ClientRepository.AllClients;
                return _clients;
            }
        }

        // RelayCommand - класс из пространства имен GalaSoft.MvvmLight.Command представляющий простую команду.
        RelayCommand _addClientCommand;
        public ICommand AddClient
        {
            get
            {
                if (_addClientCommand == null)
                    _addClientCommand = new RelayCommand(ExecuteAddClientCommand, CanExecuteAddClientCommand);
                return _addClientCommand;
            }
        }

        public void ExecuteAddClientCommand()
        {
            Clients.Add(_currentClient);
            CurrentClient = null;
        }

        public bool CanExecuteAddClientCommand()
        {
            if (string.IsNullOrEmpty(CurrentClient.FirstName) ||
                string.IsNullOrEmpty(CurrentClient.LastName))
                return false;
            return true;
        }
    }
}

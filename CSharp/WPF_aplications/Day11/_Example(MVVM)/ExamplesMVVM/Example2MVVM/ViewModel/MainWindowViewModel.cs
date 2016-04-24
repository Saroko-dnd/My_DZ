using System.Collections.ObjectModel;
using MVVM_Sample.Model;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using MVVM_Sample.Infrastructure;

namespace MVVM_Sample.ViewModel
{ 
    public class MainWindowViewModel : ViewModelBase
    {

        // Текущий пользователь для добавления 
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
                OnPropertyChanged("CurrentClient");
            }
        }


        // Свойство для привязки 
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


        // Команда для добавления нового пользователя!
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

        public void ExecuteAddClientCommand(object parameter)
        {
            Clients.Add(CurrentClient);
            CurrentClient = null;
        }

        public bool CanExecuteAddClientCommand(object parameter)
        {
            if (string.IsNullOrEmpty(CurrentClient.FirstName) ||
                string.IsNullOrEmpty(CurrentClient.LastName))
                return false;
            return true;
        }


        // Освобождение памяти!
        protected override void OnDispose()
        {
            this.Clients.Clear();
        }
    }
}

using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MVVMLight_Sample.Model
{
    public static class ClientRepository
    {
        private static ObservableCollection<Client> _clients;

        public static ObservableCollection<Client> AllClients 
        {
            get 
            {
                if (_clients == null)
                    _clients = GenerateClientRepository();
                return _clients;
            }
        }

        private static ObservableCollection<Client> GenerateClientRepository()
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client>();
            clients.Add(new Client("Jhon", "Doe"));
            clients.Add(new Client("Tom", "Ronald"));
            clients.Add(new Client("Jane", "Roe"));
            return clients;
        }
    }
}

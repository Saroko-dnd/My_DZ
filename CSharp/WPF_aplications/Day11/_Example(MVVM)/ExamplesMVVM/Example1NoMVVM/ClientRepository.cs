using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoMVVM
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
            clients.Add(new Client("Ivan", "Ivanov"));
            clients.Add(new Client("Petr", "Petrov"));
            clients.Add(new Client("Inna", "Krot"));
            return clients;
        }
    }
}

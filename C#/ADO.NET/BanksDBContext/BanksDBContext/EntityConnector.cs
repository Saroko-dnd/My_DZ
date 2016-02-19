using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MapDBContext
{
    public static class EntityConnector
    {
        public static string ConnectionStringName = String.Empty;

        public static BankBranch LoadSelectedObjectData(string BankBranchName)
        {
            BanksDBContext BanksDatabase = new BanksDBContext(ConnectionStringName);
            //если поместить (Sender as Label).Content.ToString() в запрос то возращает исключение
            BankBranch SelectedBranch = BanksDatabase.BankBranches.Where(res => res.BranchName == BankBranchName).
                First();
            return SelectedBranch;
        }

        public static List<string> GetListOfServices()
        {
            BanksDBContext BanksDatabase = new BanksDBContext(ConnectionStringName);
            List<string> ListOfServices = new List<string>();
            foreach (Service CurrentService in BanksDatabase.Services)
            {
                ListOfServices.Add(CurrentService.Servise);
            }
            return ListOfServices;
        }
    }
}

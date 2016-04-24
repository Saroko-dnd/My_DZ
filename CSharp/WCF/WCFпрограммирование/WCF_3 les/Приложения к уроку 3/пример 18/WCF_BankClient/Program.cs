using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCF_BankClient.ServiceReference1;
using System.Transactions;

namespace WCF_BankClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                BankServiceClient proxy = new BankServiceClient();
                Console.WriteLine("Укажите сумму депозита:");
                double sum = Convert.ToDouble(Console.ReadLine());
                double result = 0;

                while (sum > 0)
                {
                    proxy.ToDeposit(sum);
                    result = proxy.GetBalance();
                    Console.WriteLine("Депозит = {0}", result);
                    Console.WriteLine("Укажите сумму депозита:");
                    sum = Convert.ToDouble(Console.ReadLine());
                }
                scope.Complete();
            }
            Console.WriteLine("Для завершения нажмите<ENTER>\n\n");
            Console.ReadLine();
        }
    }
}

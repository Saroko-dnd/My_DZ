using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF_BankService
{
    //контракт службы	
    [ServiceContract]
    public interface IBankService
    {
        [OperationContract]

        void ToDeposit(double sum);
        [OperationContract]
        double GetBalance();
    }

    //класс службы	
    public class BankService : IBankService
    {
        static int id = 0;		//для нумерации создаваемых счетов
        public double Balance;	//баланс счета

        //создание нового счета
        public BankService()
        {
            ++id;
            Console.WriteLine("Создан счет № " + id.ToString());
        }
        //перевод денег на созданный счет
        //перевод денег на созданный счет
        [OperationBehavior(TransactionScopeRequired = true,TransactionAutoComplete = true)]
        public void ToDeposit(double sum)
        {
            Console.WriteLine("Изменение баланса");
            this.Balance += sum;
            double bal = GetBalance();
            Console.WriteLine("Баланс: {0}", bal);
        }

        //вывод текущего баланса счета
        public double GetBalance()
        {
            return Balance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh = new ServiceHost(typeof(BankService));

            sh.Open();
            Console.WriteLine("Для завершения нажмите<ENTER>\n\n");
            Console.ReadLine();
            sh.Close();
        }
    }
}

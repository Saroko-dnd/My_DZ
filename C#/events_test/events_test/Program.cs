using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace events_test
{
    class Program
    {

        public delegate void SampleEventHandler(deal new_deal);
        static public event SampleEventHandler deal_was_created;
        public static Random main_random = new Random();
        public const int sum_of_cheap_deal = 500000;

        static void Main(string[] args)
        {


            List<organization> array_of_organizations = new List<organization>();
            array_of_organizations.Add(new organization(50,"ASUS"));
            array_of_organizations.Add(new organization(75, "GRAB_corporation"));
            array_of_organizations.Add(new organization(25, "IBM"));
            bank our_bank = new bank(array_of_organizations);
            const int min_sum_of_money = 2;
            const int max_sum_of_money = 1000000;
            int sum_of_money = 0;
            int index_of_organization = -1;

            deal_was_created += our_bank.get_deal;
            deal_was_created += our_bank.add_deal_to_list_of_protocols;

            for (int counter = 0; counter < 100; ++counter)
            {
                sum_of_money = main_random.Next(min_sum_of_money, max_sum_of_money);
                index_of_organization = main_random.Next(3);
                deal_was_created(array_of_organizations[index_of_organization].generate_deal(sum_of_money));
            }
            Console.WriteLine("amount of useless audit " + our_bank.bank_auditor.useless_audit_count);
            Console.WriteLine("amount of useful audit " + our_bank.bank_auditor.useful_audit_count);
            Console.ReadKey();

        }

    }

}

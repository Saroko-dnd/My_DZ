using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace events_test
{
    class organization
    {
        /// <summary>
        /// вероятность честной сделки в процентах (от 0 до 100)
        /// </summary>
        public int honesty;
        /// <summary>
        /// модификатор вероятности незаконной сделки для больших сумм денег
        /// </summary>
        public int greed_value;
        /// <summary>
        /// кредит доверия к компании (влияет на частоту аудита в отношении сделок этой компании)
        /// </summary>
        public int credit_of_trust = 1;
        public string name_of_organization;
        public void change_credit_of_trust (int number_to_add)
        {
            credit_of_trust += number_to_add;
            if (credit_of_trust <= 0)
                credit_of_trust = 1;
            if (credit_of_trust > 95)
                credit_of_trust = 95;
        }
        public deal generate_deal(int new_sum_of_deal)
        {
            if (new_sum_of_deal <= Program.sum_of_cheap_deal)
                return new deal(name_of_organization, new_sum_of_deal, new_sum_of_deal % 100 <= honesty);
            else
                return new deal(name_of_organization, new_sum_of_deal, (new_sum_of_deal % 100 + greed_value) <= honesty);
        }
        public organization(int new_honesty, string new_name)
        {
            honesty = new_honesty;
            greed_value = 500 / honesty;
            name_of_organization = new_name;
        }   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace events_test
{
    class bank
    {
        public List<protocol> list_of_deals = new List<protocol>();
        public audit bank_auditor = new audit();
        List<organization> all_organizations;
        public delegate int SampleEventHandler(deal new_deal);
        public event SampleEventHandler perform_audit;
        /// <summary>
        /// вычисляет индкс организации в массиве all_organizations
        /// </summary>
        int find_index_of_organization(string name_of_organization)
        {
            for (int counter = 0; counter < all_organizations.Count(); ++counter)
            {
                if (all_organizations[counter].name_of_organization == name_of_organization)
                {
                    return counter;
                }
            }
            return -1;
        }

        public void get_deal(deal current_deal)
        {
            int index_of_organization = find_index_of_organization(current_deal.organization_name);
            if (index_of_organization == -1)
            {
                throw new System.ArgumentException("bank know nothing about current organization");     
            }
            else
            {
                int risk_mod = 0;
                if (current_deal.sum_of_deal > Program.sum_of_cheap_deal)
                {
                    risk_mod = 200 / all_organizations[index_of_organization].credit_of_trust;
                }
                int number_for_decision = Program.main_random.Next(100) + risk_mod;
                if (number_for_decision > all_organizations[index_of_organization].credit_of_trust)
                {
                    int check_of_deal = perform_audit(current_deal);
                    if (check_of_deal < 0)
                    {
                        all_organizations[index_of_organization].change_credit_of_trust(-10);
                    }
                    else if (check_of_deal > 1)
                    {
                        all_organizations[index_of_organization].change_credit_of_trust(5);
                    }
                }
            }
        }

        public void add_deal_to_list_of_protocols(deal current_deal)
        {
            list_of_deals.Add(new protocol(current_deal.organization_name, current_deal.sum_of_deal));
        }

        public bank (List<organization> new_list_of_organizations)
        {
            all_organizations = new_list_of_organizations;
            perform_audit += bank_auditor.do_audit;
        }
    }
}

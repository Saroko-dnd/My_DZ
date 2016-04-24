using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace events_test
{
    class audit
    {
        public int useless_audit_count = 0;
        public int useful_audit_count = 0;
        /// <summary>
        /// шанс на ошибочный результат аудита (от 0 до 10 процентов)
        /// </summary>
        public int chance_of_mistake;
        public int do_audit(deal current_deal)
        {
            int try_to_find_out = Program.main_random.Next(100);
            if (try_to_find_out <= chance_of_mistake)
            {
                useless_audit_count += 1;
                return 0;
            }
            else if (current_deal.fair_deal)
            {
                useless_audit_count += 1;
                return 1;
            }
            else
            {
                useful_audit_count += 1;
                return -1;
            }
        }
        public audit()
        {
            System.DateTime time_at_this_moment = new System.DateTime();
            chance_of_mistake = time_at_this_moment.Millisecond % 11;
        }
    }
}

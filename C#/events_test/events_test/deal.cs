using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace events_test
{
    class deal
    {
        public string organization_name;
        public int sum_of_deal;
        /// <summary>
        /// true если сделка законная, false если незаконная
        /// </summary>
        public bool fair_deal;
        public deal (string new_name, int new_sum, bool fair_or_not)
        {
            organization_name = new_name;
            sum_of_deal = new_sum;
            fair_deal = fair_or_not;
        }
    }
}

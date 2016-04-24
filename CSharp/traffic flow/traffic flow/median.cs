using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traffic_flow
{
    public static class LINQExtension
    {
        public static int Median(this IEnumerable<int> source)
        {
            try
            {
                if (source.Count() == 0)
                {
                    throw new InvalidOperationException("Cannot compute median for an empty set.");
                }

                var sortedList = from number in source
                                 orderby number
                                 select number;

                int itemIndex = (int)sortedList.Count() / 2;

                if (sortedList.Count() % 2 == 0)
                {
                    // Even number of items.
                    return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;
                }
                else
                {
                    // Odd number of items.
                    return sortedList.ElementAt(itemIndex);
                }
            }
            catch (InvalidOperationException current_exception)
            {
                return -1;
            }
        }
    }
}

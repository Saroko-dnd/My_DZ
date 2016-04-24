using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Enumerator
{
    class person : IEnumerable
    {
        public List<string> current_strings = new List<string> { "first", "second", "third" };
        public System.Collections.IEnumerator GetEnumerator()
        {
            for (int counter = 0; counter < current_strings.Count(); ++counter)
            {
                yield return current_strings[counter];
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            person test_person = new person();
            foreach (string cur_str in test_person)
            {
                Console.WriteLine(cur_str);
            }
            Console.ReadKey();
        }
    }
}

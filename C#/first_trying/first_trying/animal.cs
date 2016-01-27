using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_trying
{
    interface IFlyers
    {
        void to_fly();
    }
    abstract class animal
    {
        public string name;
        abstract public void voice();
        public animal (string new_name)
        {
            name = new_name;
        }
    }

    class bird : animal, IFlyers
    {
        public void to_fly()
        {
            Console.Write(name);
            Console.Write(" ");
            Console.Write("can fly\n");
        }
        override public void voice()
        {
            Console.WriteLine("bird say ");
        }

        public bird (string current_string) : base(current_string)
        {
            name = current_string;
        }  
    }

    class cat : animal
    {
        override public void voice()
        {
            Console.WriteLine("cat say ");
        }

        public cat(string current_string) : base(current_string)
        {
            name = current_string;
        }
    }
}

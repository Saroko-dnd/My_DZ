using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLINQexample
{
    public class TestDataClass
    {
        private int age;

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public TestDataClass(int NewAge, string NewName)
        {
            Age = NewAge;
            Name = NewName;
        }
    }
}

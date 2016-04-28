using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternBuilder
{
    public class Car
    {
        string body;

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        int engine;

        public int Engine
        {
            get { return engine; }
            set { engine = value; }
        }
        int wheels;

        public int Wheels
        {
            get { return wheels; }
            set { wheels = value; }
        }
        string transmission;

        public string Transmission
        {
            get { return transmission; }
            set { transmission = value; }
        }
    }
}

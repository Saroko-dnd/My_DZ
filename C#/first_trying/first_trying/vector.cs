using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_trying
{
    class vector
    {
        public int[] our_vector;
        static public vector multiplication(vector first,vector second)
        {

            vector buf = new vector(0,0,0);
            buf.our_vector[0] = (first.our_vector[1] * second.our_vector[2]) - 
                (first.our_vector[2] * second.our_vector[1]);
            buf.our_vector[1] = (first.our_vector[2] * second.our_vector[0]) - 
                (first.our_vector[0] * second.our_vector[2]);
            buf.our_vector[2] = (first.our_vector[0] * second.our_vector[1]) - 
                (first.our_vector[1] * second.our_vector[0]);
            return buf;
        }
        public vector(int x,int y,int z)
        {
            our_vector = new int[3];
            our_vector[0] = x;
            our_vector[1] = y;
            our_vector[2] = z;
        }
    }
}

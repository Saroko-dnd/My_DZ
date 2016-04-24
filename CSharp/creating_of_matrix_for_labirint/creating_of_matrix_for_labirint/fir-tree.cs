using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creating_of_matrix_for_labirint
{
    public class fir_tree
    {
        public List<int> polygon = new List<int>();
        public List<int> rectangle = new List<int>();

        public fir_tree (List<int> new_polygon, List<int> new_rectangle)
        {
            polygon = new_polygon;
            rectangle = new_rectangle;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_test
{
    public class subject
    {
        public string name;
        public List<string> all_themes;
        public subject()
        {

        }
        public subject(string new_name, List<string> new_theme)
        {
            name = new_name;
            all_themes = new_theme;
        }
    }
}

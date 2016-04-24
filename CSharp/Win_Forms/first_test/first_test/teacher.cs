using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_test
{
    public class teacher
    {
        public string name;
        public string login;
        public int hash_of_password;
        public teacher (string new_name,string new_login,string new_password)
        {
            name = new_name;
            login = new_login;
            hash_of_password = new_password.GetHashCode();
        }
        public teacher()
        {

        }
    }
}

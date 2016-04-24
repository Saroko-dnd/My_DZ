using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForWCFchat
{
    public class ClientName
    {
        private string CurrentClientName_;

        public string CurrentClientName
        {
            get { return CurrentClientName_; }
            set { CurrentClientName_ = value; }
        }

        public ClientName(string NewClientName)
        {
            CurrentClientName = NewClientName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfChatService
{
    public class ClientJoinInfo
    {
        public IClientCallback CurrentClientCallback;
        public string CurrentClientName;

        public ClientJoinInfo(string NewClientName, IClientCallback NewClientCallback)
        {
            CurrentClientName = NewClientName;
            CurrentClientCallback = NewClientCallback;
        }
    }
}

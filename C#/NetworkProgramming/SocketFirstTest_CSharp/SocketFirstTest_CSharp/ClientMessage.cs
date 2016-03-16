using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketFirstTest_CSharp
{
    public class ClientMessage
    {
        public string ClientName = string.Empty;
        public string Message = string.Empty;

        public ClientMessage(string NewClientName, string NewMessage)
        {
            ClientName = NewClientName;
            Message = NewMessage;
        }
    }
}

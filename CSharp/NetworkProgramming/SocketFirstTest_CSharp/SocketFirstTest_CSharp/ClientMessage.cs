using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketFirstTest_CSharp
{
    public class ClientMessage
    {
        public string SenderName = string.Empty;
        public string ClientName = string.Empty;
        public string Message = string.Empty;

        public ClientMessage(string NewClientName, string NewSenderName, string NewMessage)
        {
            SenderName = NewSenderName;
            ClientName = NewClientName;
            Message = NewMessage;
        }
    }
}

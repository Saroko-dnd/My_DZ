using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithoutServer
{
    public class ClientAndMessage
    {
        private string ClientName_;

        public string ClientName
        {
            get { return ClientName_; }
            set { ClientName_ = value; }
        }

        private string ClientMessage_;

        public string ClientMessage
        {
            get { return ClientMessage_; }
            set { ClientMessage_ = value; }
        }
        public ClientAndMessage(string NewClientName, string NewClientMessage)
        {
            ClientName = NewClientName;
            ClientMessage = NewClientMessage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.Models
{
    public class UserChatPage
    {
        private string userName = "User";

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string userMessage = string.Empty;

        public string UserMessage
        {
            get { return userMessage; }
            set { userMessage = value; }
        }

        private List<UserMessage> lastMessages;

        public List<UserMessage> LastMessages
        {
            get { return lastMessages; }
            set { lastMessages = value; }
        }
    }
}
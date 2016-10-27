using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat
{
    public class UserMessage
    {
        private string message = string.Empty;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private string userName = string.Empty;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public UserMessage(string NewMessage, string NewUserName)
        {
            Message = NewMessage;
            UserName = NewUserName;
            Date = DateTime.Now;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.ClassesForChat
{
    public interface IAccessorToUserSession
    {
        void AddNewMessage(string TextOfMessage, string UserName);
        IEnumerable<UserMessage> GetMessages();
    }
}
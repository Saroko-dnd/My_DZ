using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.ClassesForChat
{
    public interface IAccessorToApplicationForChat
    {
        IEnumerable<UserMessage> GetMessages();
        void AddNewMessageToChat(string TextOfMessage, string UserName);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.ClassesForChat
{
    public class AccessorToApplicationForChat : IAccessorToApplicationForChat
    {       
        private static readonly int MaxAmountOfMessages = 20;
        private static readonly string KeyToChatMessages = "ChatMessages";
        private static readonly object GatesToMessages = new object();

        public IEnumerable<UserMessage> GetMessages()
        {
            return (HttpContext.Current.Application[KeyToChatMessages] as List<UserMessage>);
        }

        public void AddNewMessageToChat(string TextOfMessage, string UserName)
        {
            List<UserMessage> CurrentListOfMessages = (HttpContext.Current.Application[KeyToChatMessages] as List<UserMessage>);
            lock(GatesToMessages)
            {
                UserMessage NewUserMessage = new UserMessage(TextOfMessage, UserName);
                CurrentListOfMessages.Add(NewUserMessage);
                if (CurrentListOfMessages.Count > MaxAmountOfMessages)
                {
                    CurrentListOfMessages.RemoveAt(0);
                }
            }
        }

        public AccessorToApplicationForChat()
        {
            if (HttpContext.Current.Application[KeyToChatMessages] == null)
            {
                List<UserMessage> NewListOfMessages = new List<UserMessage>();
                NewListOfMessages.Add(new UserMessage("First test user message","First test user name"));
                NewListOfMessages.Add(new UserMessage("Second test user message", "Second test user name"));
                NewListOfMessages.Add(new UserMessage("Third test user message", "Third test user name"));
                HttpContext.Current.Application[KeyToChatMessages] = NewListOfMessages;
            }
        }
    }
}
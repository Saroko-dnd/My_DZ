using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.ClassesForChat
{
    public class AccessorToUserSession
    {
        private static readonly int MaxAmountOfMessages = 10;
        private static readonly string KeyForMessagesCreatedByCurrentUser = "MessagesCreatedByCurrentUser";

        private HttpSessionStateBase CurrentUserSession;

        public void AddNewMessage(string TextOfMessage, string UserName)
        {
            List<UserMessage> CurrentListOfUserMessages = (CurrentUserSession[KeyForMessagesCreatedByCurrentUser] as List<UserMessage>);
            UserMessage NewUserMessage = new UserMessage(TextOfMessage, UserName);
            CurrentListOfUserMessages.Add(NewUserMessage);
            if (CurrentListOfUserMessages.Count > MaxAmountOfMessages)
            {
                CurrentListOfUserMessages.RemoveAt(0);
            }
        }

        public List<UserMessage> GetMessages()
        {
             return (CurrentUserSession[KeyForMessagesCreatedByCurrentUser] as List<UserMessage>);
        }

        public AccessorToUserSession(HttpSessionStateBase UserSession)
        {
            CurrentUserSession = UserSession;
            if (CurrentUserSession[KeyForMessagesCreatedByCurrentUser] == null)
            {
                CurrentUserSession[KeyForMessagesCreatedByCurrentUser] = new List<UserMessage>();
            }
        }

    }
}
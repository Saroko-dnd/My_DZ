using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.Models
{
    public class UserChatPage
    {
        private static readonly string sortMessagesByUserName = "Sort messages by user name";
        private static readonly string sortMessagesByDate = "Sort messages by date";
        private static readonly string doNotSortMessages = "No sorting";

        private string userName = "User";
        private bool receiveOnlyMessagesCreatedByCurrentUser = false;
        private string userMessage = string.Empty;
        private string sortingForMessages = DoNotSortMessages;
        private IEnumerable<UserMessage> lastMessages;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string UserMessage
        {
            get { return userMessage; }
            set { userMessage = value; }
        }

        public string SortingForMessages
        {
            get
            {
                return sortingForMessages;
            }

            set
            {
                sortingForMessages = value;
            }
        }

        public IEnumerable<UserMessage> LastMessages
        {
            get { return lastMessages; }
            set { lastMessages = value; }
        }

        public static string SortMessagesByUserName
        {
            get
            {
                return sortMessagesByUserName;
            }
        }

        public static string SortMessagesByDate
        {
            get
            {
                return sortMessagesByDate;
            }
        }

        public static string DoNotSortMessages
        {
            get
            {
                return doNotSortMessages;
            }
        }

        public bool ReceiveOnlyMessagesCreatedByCurrentUser
        {
            get
            {
                return receiveOnlyMessagesCreatedByCurrentUser;
            }

            set
            {
                receiveOnlyMessagesCreatedByCurrentUser = value;
            }
        }
    }
}
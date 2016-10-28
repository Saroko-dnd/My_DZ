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

        private string sortingForMessages = DoNotSortMessages;

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

        private List<UserMessage> lastMessages;

        public List<UserMessage> LastMessages
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
    }
}
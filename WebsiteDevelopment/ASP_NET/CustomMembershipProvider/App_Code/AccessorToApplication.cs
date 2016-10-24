using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccessorToApplication
/// </summary>
public class AccessorToApplication
{
    private static readonly string KeyForChatMessages = "GlobalChat";
    private static bool ChatWasInitialized = false;
    private static object GatesToChatCreation = new object();

    private static int amountOfMessagesForSendingToClientAtOnce = 20;

    public static int AmountOfMessagesForSendingToClientAtOnce
    {
        get { return amountOfMessagesForSendingToClientAtOnce; }
        private set { amountOfMessagesForSendingToClientAtOnce = value; }
    }

    private static int maxAmountOfMessagesInMemory = 100;

    public static int MaxAmountOfMessagesInMemory
    {
        get { return maxAmountOfMessagesInMemory; }
        private set { maxAmountOfMessagesInMemory = value; }
    }

    private object GatesToApplication = new object();

    public void AddNewMessageToChat(UserMessage NewUserMessage)
    {
        lock (GatesToApplication)
        {
            List<UserMessage> BufferForAllMessages = (HttpContext.Current.Application[KeyForChatMessages] as List<UserMessage>);
            BufferForAllMessages.Add(NewUserMessage);
            if (BufferForAllMessages.Count > MaxAmountOfMessagesInMemory)
            {
                BufferForAllMessages.RemoveAt(0);
            }
        }
    }

    public List<UserMessage> GetLastChatMessages()
    {
        List<UserMessage> BufferForAllMessages = (HttpContext.Current.Application[KeyForChatMessages] as List<UserMessage>);
        return BufferForAllMessages.Skip(Math.Max(0, BufferForAllMessages.Count() - AmountOfMessagesForSendingToClientAtOnce)).ToList();
    }

    public AccessorToApplication()
	{
        if (!ChatWasInitialized)
        {
            lock (GatesToChatCreation)
            {
                if (!ChatWasInitialized)
                {
                    HttpContext.Current.Application[KeyForChatMessages] = new List<UserMessage>();
                    ChatWasInitialized = true;
                }
            }
        }
    }
}
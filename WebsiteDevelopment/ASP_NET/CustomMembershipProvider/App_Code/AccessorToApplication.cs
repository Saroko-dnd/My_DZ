using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccessorToApplication
/// </summary>
public class AccessorToApplication
{
    private HttpApplicationState CurrentApplication;

    private int amountOfMessagesForSendingToClientAtOnce;

    public int AmountOfMessagesForSendingToClientAtOnce
    {
        get { return amountOfMessagesForSendingToClientAtOnce; }
        set { amountOfMessagesForSendingToClientAtOnce = value; }
    }

    private static int maxAmountOfMessagesInMemory = 100;

    public static int MaxAmountOfMessagesInMemory
    {
        get { return AccessorToApplication.maxAmountOfMessagesInMemory; }
        set { AccessorToApplication.maxAmountOfMessagesInMemory = value; }
    }

    private object GatesToApplication = new object();

    public void AddNewMessageToChat(UserMessage NewUserMessage)
    {
        lock (GatesToApplication)
        {
            List<UserMessage> BufferForAllMessages = (CurrentApplication["GlobalChat"] as List<UserMessage>);
            BufferForAllMessages.Add(NewUserMessage);
            if (BufferForAllMessages.Count > MaxAmountOfMessagesInMemory)
            {
                BufferForAllMessages.RemoveAt(0);
            }
        }
    }

    public List<UserMessage> GetLastChatMessages()
    {
        List<UserMessage> BufferForAllMessages = (CurrentApplication["GlobalChat"] as List<UserMessage>);
        return BufferForAllMessages.Skip(Math.Max(0, BufferForAllMessages.Count() - AmountOfMessagesForSendingToClientAtOnce)).ToList();
    }

    public AccessorToApplication(HttpApplicationState Application, int AmountOfMessagesToGet)
	{
        AmountOfMessagesForSendingToClientAtOnce = AmountOfMessagesToGet;
        CurrentApplication = Application;
	}

    public AccessorToApplication(HttpApplicationState Application)
    {
        CurrentApplication = Application;
    }
}
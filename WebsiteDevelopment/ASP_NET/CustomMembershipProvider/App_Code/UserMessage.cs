using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserMessage
/// </summary>
public class UserMessage
{
    private string currentMessage;

    public string CurrentMessage
    {
        get { return currentMessage; }
        set { currentMessage = value; }
    }
    private string messageSource;

    public string MessageSource
    {
        get { return messageSource; }
        set { messageSource = value; }
    }
    public UserMessage(string NewMessage, string UserName)
    {
        CurrentMessage = NewMessage;
        MessageSource = Texts.MessageFrom + " " + UserName;
    }
	public UserMessage()
	{

	}
}
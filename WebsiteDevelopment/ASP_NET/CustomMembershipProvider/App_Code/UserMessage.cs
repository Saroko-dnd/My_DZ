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
    private string currentUserName;

    public string CurrentUserName
    {
        get { return currentUserName; }
        set { currentUserName = value; }
    }
    public UserMessage(string NewMessage, string UserName)
    {
        CurrentMessage = NewMessage;
        CurrentUserName = UserName;
    }
	public UserMessage()
	{

	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

/// <summary>
/// Summary description for AccessorToSession
/// </summary>
public class AccessorToSession
{
    private static readonly string KeyForTest = "TestOnCurrentPage";
    private static readonly string KeyForUserScoreCollection = "CurrentUserScoreCollection";
    private static readonly string KeyForUserControlForTest = "CurrentControlForTest";

    private HttpSessionState CurrentSession;

    public UserControl CurrentControlForTest
    {
        get
        {
            return (CurrentSession[KeyForUserControlForTest] as UserControl);
        }
        set
        {
            CurrentSession[KeyForUserControlForTest] = value;
        }
    }

    public Test CurrentTest
    {
        get
        {
            return (CurrentSession[KeyForTest] as Test);
        }
        set
        {
            CurrentSession[KeyForTest] = value;
        }
    }

    public Dictionary<uint,uint> CurrentUserScoreCollection
    {
        get
        {
            return (Dictionary<uint, uint>)CurrentSession[KeyForUserScoreCollection];
        }
        set
        {
            CurrentSession[KeyForUserScoreCollection] = value;
        }
    }
	public AccessorToSession(HttpSessionState NewSession)
	{
        CurrentSession = NewSession;
	}
}
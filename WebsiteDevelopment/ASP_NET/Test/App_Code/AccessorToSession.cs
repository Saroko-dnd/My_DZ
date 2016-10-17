using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Summary description for AccessorToSession
/// </summary>
public class AccessorToSession
{
    private HttpSessionState CurrentSession;
    public Test CurrentTest
    {
        get
        {
            return (CurrentSession["TestOnCurrentPage"] as Test);
        }
        set
        {
            CurrentSession["TestOnCurrentPage"] = value;
        }
    }
	public AccessorToSession(HttpSessionState NewSession)
	{
        CurrentSession = NewSession;
	}
}
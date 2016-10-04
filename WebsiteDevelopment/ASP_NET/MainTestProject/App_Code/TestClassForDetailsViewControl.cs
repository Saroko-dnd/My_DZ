using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestClassForDetailsViewControl
/// </summary>
public class TestClassForDetailsViewControl
{
    private string firstName = string.Empty;
    private string secondName = string.Empty;

    public string SecondName
    {
        get { return secondName; }
        set { secondName = value; }
    }

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }


	public TestClassForDetailsViewControl(string NewFirstName, string NewSecondName)
	{
        FirstName = NewFirstName;
        SecondName = NewSecondName;
	}
}
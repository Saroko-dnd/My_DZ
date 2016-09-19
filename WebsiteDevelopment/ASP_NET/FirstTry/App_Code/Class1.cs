using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class AllGuestsInfo
{
    private static List<string> Storage = null;
    public void AddGuest(string NewGuest)
    {
        Storage.Add(NewGuest);
    }

    public List<string> GetAllGuests()
    {
        return Storage;
    }
	static AllGuestsInfo()
	{
        if (Storage == null)
        {
            Storage = new List<string>();
        }
	}
}
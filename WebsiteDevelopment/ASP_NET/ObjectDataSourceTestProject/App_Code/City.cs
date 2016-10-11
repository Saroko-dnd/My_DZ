using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for City
/// </summary>
public class City
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
	public City(string NewCityName)
	{
        Name = NewCityName;
	}
}
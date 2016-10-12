using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Summary description for CitiesDataSource
/// </summary>
public class CitiesDataSource
{
    private List<City> ListOfCities = new List<City>();

    public IEnumerable<City> GetListOfCities()
    {
        return ListOfCities;
    }

    public CitiesDataSource()
	{
        XmlDocument doc = new XmlDocument();
        string FullPathToXmlFileWithCities = HttpRuntime.AppDomainAppPath + @"XMLFolder\Cities.xml";
        doc.Load(FullPathToXmlFileWithCities);
        XmlNodeList XmlCityNodes = doc.SelectNodes(@"/Cities/City");
        foreach (XmlNode node in XmlCityNodes)
        {
            ListOfCities.Add(new City(node.Attributes["Name"].Value));
        }
    }
}
using System;
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
public class CitiesDataSource : IHierarchicalDataSource
{
    private List<City> CityNames = new List<City>() { new City("City name 1"), new City("City name 2"), new City("City name 3"),new City("City name 4"), new City("City name 5") };
    public IEnumerable<City> GetListOfCities()
    {
        return CityNames;
    }

    public XmlDataSource GetDataSetOfCities()
    {
        DataSet SetOfCityNames = new DataSet();
        SetOfCityNames.Tables.Add("City");
        SetOfCityNames.Tables[0].Columns.Add("Name");
        foreach (City CurrentCity in CityNames)
        {
            SetOfCityNames.Tables[0].Rows.Add(CurrentCity.Name);
        }
        XmlDataSource CitiesAsXml = new XmlDataSource();
        CitiesAsXml.Data = SetOfCityNames.GetXml();
        return CitiesAsXml;
    }
	public CitiesDataSource()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public event EventHandler DataSourceChanged;

    public HierarchicalDataSourceView GetHierarchicalView(string viewPath)
    {
        throw new NotImplementedException();
    }
}
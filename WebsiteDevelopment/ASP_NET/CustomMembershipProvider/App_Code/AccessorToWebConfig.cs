using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Сводное описание для AccessorToWebConfig
/// </summary>
public class AccessorToWebConfig
{
    public string NameOfDirectoryForJsonData
    {
        get
        {
            return ConfigurationManager.AppSettings["NameOfDirectoryForJsonData"].ToString();
        }
    }

    public string NameOfFileForJsonData
    {
        get
        {
            return ConfigurationManager.AppSettings["NameOfFileForJsonData"].ToString();
        }
    }

    public AccessorToWebConfig()
    {

    }
}
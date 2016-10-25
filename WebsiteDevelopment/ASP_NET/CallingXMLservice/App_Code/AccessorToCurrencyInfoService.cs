using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for AccessorToCurrencyInfoService
/// </summary>
public class AccessorToCurrencyInfoService : IAccessorToCurrencyInfoService
{
    private static readonly string ApiForGettingCurrentCurrencyData = "http://www.nbrb.by/API/ExRates/Rates?onDate=2016-7-6&Periodicity=0";

    public IEnumerable<object> GetCurrency()
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiForGettingCurrentCurrencyData);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string JsonCurrencyData = string.Empty;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                JsonCurrencyData = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return JsonConvert.DeserializeObject<List<CurrencyInfo>>(JsonCurrencyData);
        }
        catch
        {
            return null;
        }
    }

	public AccessorToCurrencyInfoService()
	{
        
	}
}
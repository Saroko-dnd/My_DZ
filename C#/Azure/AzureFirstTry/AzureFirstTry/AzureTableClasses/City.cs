using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFirstTry.AzureTableClasses
{
    public class City : TableEntity
    {
        public int Population { get; set; }
        public string DialingCode { get; set; }

        public City(string Country, string Name)
        {
            this.PartitionKey = Country;
            this.RowKey = Name;
        }

        public City()
        {

        }
    }
}

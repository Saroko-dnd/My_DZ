using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using Microsoft.WindowsAzure.Storage.Queue;
using AzureFirstTry.AzureTableClasses;

namespace AzureFirstTry
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test of Azure SQL database usage
            using (SarokoDBModel AzureSQLDatabase = new SarokoDBModel())
            {
                if (AzureSQLDatabase.Seas.Count() == 0)
                {
                    List<Sea> ListOfSeas = new List<Sea>();
                    ListOfSeas.Add(new Sea() { MaxDepthInMeters = 1400, Name = "Labrador Sea", Pirates = true });
                    ListOfSeas.Add(new Sea() { MaxDepthInMeters = 2000, Name = "Irish Sea", Pirates = false });
                    ListOfSeas.Add(new Sea() { MaxDepthInMeters = 1800, Name = "Davis Sea", Pirates = true });
                    ListOfSeas[0].Fishes.Add(new Fish() { Name = "Angler", MaxSizeInCm = 140, Predator = false, MaxWeightInGrams = 1200 });
                    ListOfSeas[1].Fishes.Add(new Fish() { Name = "Capelin", MaxSizeInCm = 50, Predator = false, MaxWeightInGrams = 400 });
                    ListOfSeas[2].Fishes.Add(new Fish() { Name = "Flagtail", MaxSizeInCm = 200, Predator = true, MaxWeightInGrams = 5000 });
                    foreach (Sea FoundSea in ListOfSeas)
                    {
                        AzureSQLDatabase.Seas.Add(FoundSea);
                    }
                    AzureSQLDatabase.SaveChanges();
                }
                else
                {
                    Console.WriteLine(AzureSQLDatabase.Fishes.First().MaxWeightInGrams.ToString());
                }
            }

            //Test of Azure data storage (TABLES)
            CloudStorageAccount MyStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageConnectionString"));         
            CloudTableClient TableClientForMyStorageAccount = MyStorageAccount.CreateCloudTableClient();
            CloudTable MyAzureTable = TableClientForMyStorageAccount.GetTableReference("TestTable");
            MyAzureTable.CreateIfNotExists();
            TableQuery<City> QueryToTestTable = new TableQuery<City>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Belarus"));
            IEnumerable<City> ListOfCitiesFoundInAzureTable = MyAzureTable.ExecuteQuery(QueryToTestTable);
            if (ListOfCitiesFoundInAzureTable.Count() > 0)
            {
                Console.WriteLine("Cities found in azure table:");
                foreach (City FoundCity in ListOfCitiesFoundInAzureTable)
                {
                    Console.WriteLine("Country: {0} City: {1} Population: {2} Dialing code: {3}", FoundCity.PartitionKey, FoundCity.RowKey,
                        FoundCity.Population, FoundCity.DialingCode);
                }
            }
            else
            {
                //INSERTION
                List<City> ListOfCities = new List<City>();
                ListOfCities.Add(new City("Belarus", "Minsk") { Population = 1959781, DialingCode = "+37517" });
                ListOfCities.Add(new City("Belarus", "Brest") { Population = 340141, DialingCode = "+375162" });
                ListOfCities.Add(new City("Belarus", "Grodno") { Population = 365610, DialingCode = "+375152" });
                //Bath operation accepts only entities with same partition key
                TableBatchOperation BatchOperation = new TableBatchOperation();
                foreach (City FoundCity in ListOfCities)
                {
                    BatchOperation.Insert(FoundCity);
                }
                MyAzureTable.ExecuteBatch(BatchOperation);
                Console.WriteLine("Some cities were added to azure table.");
            }

            //Test of Azure data storage (QUEUE)
            CloudQueueClient QueueClientForMyStorageAccount = MyStorageAccount.CreateCloudQueueClient();
            CloudQueue MyAzureQueue = QueueClientForMyStorageAccount.GetQueueReference("test-queue");
            MyAzureQueue.CreateIfNotExists();
            Console.WriteLine("Test queue was created successfully.");

            Console.ReadKey();
        }
    }
}

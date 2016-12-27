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
using Microsoft.ServiceBus.Messaging;
using System.Configuration;
using System.Net.Mail;

namespace AzureFirstTry
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test of Azure SQL database usage
            #region AzureSqlTest
            Console.WriteLine("--AZURE SQL TEST--");
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
                    Console.WriteLine("Seas and fishes were added to Azure SQL database.");
                }
            }
            using (SarokoDBModel AzureSQLDatabase = new SarokoDBModel())
            {
                Console.WriteLine("Seas found in Azure SQL database:");
                foreach (Sea FoundSea in AzureSQLDatabase.Seas.ToList())
                {
                    Console.WriteLine("Name: {0} Max depth in meters: {1} Pirates: {2}", FoundSea.Name, FoundSea.MaxDepthInMeters.ToString(), FoundSea.Pirates.ToString());
                }
            }
            #endregion

            //Test of Azure data storage (TABLES)
            #region AzureTableTest
            Console.WriteLine("--AZURE TABLE TEST--");
            CloudStorageAccount MyStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageConnectionString"));
            CloudTableClient TableClientForMyStorageAccount = MyStorageAccount.CreateCloudTableClient();
            CloudTable MyAzureTable = TableClientForMyStorageAccount.GetTableReference("TestTable");
            MyAzureTable.CreateIfNotExists();
            TableQuery<City> QueryToTestTable = new TableQuery<City>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Belarus"));
            IEnumerable<City> ListOfCitiesFoundInAzureTable = MyAzureTable.ExecuteQuery(QueryToTestTable);
            if (ListOfCitiesFoundInAzureTable.Count() == 0)
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
                ListOfCitiesFoundInAzureTable = MyAzureTable.ExecuteQuery(QueryToTestTable);
                Console.WriteLine("Cities found in azure table:");
                foreach (City FoundCity in ListOfCitiesFoundInAzureTable)
                {
                    Console.WriteLine("Country: {0} City: {1} Population: {2} Dialing code: {3}", FoundCity.PartitionKey, FoundCity.RowKey,
                        FoundCity.Population, FoundCity.DialingCode);
                }
            }
            else
            {
                Console.WriteLine("Cities found in azure table:");
                foreach (City FoundCity in ListOfCitiesFoundInAzureTable)
                {
                    Console.WriteLine("Country: {0} City: {1} Population: {2} Dialing code: {3}", FoundCity.PartitionKey, FoundCity.RowKey,
                        FoundCity.Population, FoundCity.DialingCode);
                }
            }
            #endregion

            //Test of Azure data storage (QUEUE)
            #region AzureQueueTest
            Console.WriteLine("--AZURE QUEUE TEST--");
            CloudQueueClient QueueClientForMyStorageAccount = MyStorageAccount.CreateCloudQueueClient();
            CloudQueue MyAzureQueue = QueueClientForMyStorageAccount.GetQueueReference("test-queue");
            MyAzureQueue.CreateIfNotExists();
            List<CloudQueueMessage> ListOfMessagesForQueue = new List<CloudQueueMessage>();
            ListOfMessagesForQueue.Add(new CloudQueueMessage("First message"));
            ListOfMessagesForQueue.Add(new CloudQueueMessage("Second message"));
            ListOfMessagesForQueue.Add(new CloudQueueMessage("Third message"));
            foreach (CloudQueueMessage FoundMessage in ListOfMessagesForQueue)
            {
                MyAzureQueue.AddMessage(FoundMessage);
            }
            Console.WriteLine("Messages were added to queue.");
            CloudQueueMessage RetrievedMessage;
            for (int CounterOfMessages = 0; CounterOfMessages < 3; ++CounterOfMessages)
            {
                RetrievedMessage = MyAzureQueue.GetMessage();
                Console.WriteLine(RetrievedMessage.AsString);
                MyAzureQueue.DeleteMessage(RetrievedMessage);
            }
            Console.WriteLine("All messages were retrieved from queue.");
            #endregion

            //Test of Azure service bus (QUEUE)
            #region AzureServiceBusQueueTest
            Console.WriteLine("-AZURE SERVICE BUS QUEUE TEST--");
            string ServiceBusConnectionString = ConfigurationManager.AppSettings["AzureServiceBusConnectionString"];
            string ServiceBusTestQueue = "test-queue";
            QueueClient ClientForServiceBusQueue = QueueClient.CreateFromConnectionString(ServiceBusConnectionString, ServiceBusTestQueue);
            string LastMessageInQueue = "Third test message for service bus queue.";
            List<BrokeredMessage> ListOfMessagesForServiceBusQueue = new List<BrokeredMessage>();
            ListOfMessagesForServiceBusQueue.Add(new BrokeredMessage("First test message for service bus queue."));
            ListOfMessagesForServiceBusQueue.Add(new BrokeredMessage("Second test message for service bus queue."));
            ListOfMessagesForServiceBusQueue.Add(new BrokeredMessage(LastMessageInQueue));
            ClientForServiceBusQueue.SendBatch(ListOfMessagesForServiceBusQueue);
            Console.WriteLine("Test messages were added to service bus queue.");
            ClientForServiceBusQueue.OnMessage(FoundMessage =>
            {
                string MessageBody = FoundMessage.GetBody<String>();
                Console.WriteLine(String.Format("Message body: {0}", MessageBody));
                if (MessageBody == LastMessageInQueue)
                {
                    Console.WriteLine("All messages were retrieved from service bus queue.");
                    Console.WriteLine("\n--All tests successfully completed--");
                }
            });
            #endregion
        
            //Docker authorization: thaos-admin , password: 123TheGodsAreReal321
            Console.ReadKey();
        }
    }
}

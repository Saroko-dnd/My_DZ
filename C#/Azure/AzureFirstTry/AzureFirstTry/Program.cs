using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFirstTry
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2ModelFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            DeliveryModelContainer db = new DeliveryModelContainer();

            ClientSet clinet1 = new ClientSet()
            {   Name = "Ivan",
                Email = "kin@tut.by"
            };
            
            Message message1 = new Message() { Text = "ПРивет!", Date = DateTime.Now  };
            clinet1.MessageSet.Add(message1);
            Message message2 = new Message() { Text = "Пока!", Date = DateTime.Now  };
            clinet1.MessageSet.Add(message2);
                               
            db.ClientSet.Add(clinet1);
            db.SaveChanges();


            foreach (var client in db.ClientSet)
            {
                Console.WriteLine(client.Name);
                foreach (var mess in client.MessageSet)
                {
                    Console.WriteLine(mess.Text + " " + mess.Date);
                }
            }

            Console.ReadKey();
        }
    }
}

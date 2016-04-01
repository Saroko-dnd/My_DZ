using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCF_MyClient1
{
    [ServiceContract]
    public interface IMyMath
    {
        [OperationContract]
        int Add(int a, int b);
    }

    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IMyMath> factory = new ChannelFactory<IMyMath>(
                new WSHttpBinding(),
                new EndpointAddress("http://localhost/MyMath/Ep1"));
            IMyMath channel = factory.CreateChannel();
            int result = channel.Add(35, 38);
            Console.WriteLine("result: {0}", result);
            Console.WriteLine("Для завершения нажмите <ENTER>\n");
            Console.ReadLine();
            factory.Close();
        }
    }
}

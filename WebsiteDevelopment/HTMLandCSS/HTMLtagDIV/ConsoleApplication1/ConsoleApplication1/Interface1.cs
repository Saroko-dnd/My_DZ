using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WindowsService2
{
    [ServiceContract]
    public interface InterfaceMyMath
    {
        [OperationContract]
        int MatAdd(int a, int b);
    }
    public class MyMath : InterfaceMyMath
    {
        public int MatAdd(int a, int b)
        {
            return a + b;
        }
    }
}

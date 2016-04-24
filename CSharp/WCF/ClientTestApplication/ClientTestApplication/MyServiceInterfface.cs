using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestApplication
{
    [ServiceContract]
    public interface MyServiceInterfface
    {
        [OperationContract]
        int substract(int FirstNumber, int SecondNumber);
    }
}

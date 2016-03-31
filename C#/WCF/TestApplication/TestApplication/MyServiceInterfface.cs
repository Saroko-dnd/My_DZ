using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace TestApplication
{
    [ServiceContract]
    public interface MyServiceInterfface
    {
        [OperationContract]
        int substract(int FirstNumber, int SecondNumber);
    }
}

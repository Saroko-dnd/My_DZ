using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFserverForBrakingSSH
{
    
    [ServiceContract]
    public interface IServerForBreakingSSh
    {
        [OperationContract]
        public MyTask GiveTask();

        [OperationContract]
        public void TakeAnswer(MyTask My7Tas_);

    }

    public class OurServer : IServerForBreakingSSh
    {

        public void TakeAnswer(MyTask My7Tas_)
        {
            throw new NotImplementedException();
        }

        public MyTask GiveTask()
        {
            throw new NotImplementedException();
        }
    }
}

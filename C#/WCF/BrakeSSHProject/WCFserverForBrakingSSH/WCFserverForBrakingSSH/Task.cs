using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization;

namespace WCFserverForBrakingSSH
{
    [DataContract]
    public class MyTask
    {
        [DataMember]
        long Ot;
        [DataMember]
        long Do;
        [DataMember]
        string Hash;
    }
}

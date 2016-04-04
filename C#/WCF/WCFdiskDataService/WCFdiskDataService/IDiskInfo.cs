using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCFdiskDataService
{
    [ServiceContract]
    interface IDiskInfo
    {
        [OperationContract]
        string GetDriversData();
        [OperationContract]
        MainDriveInfo GetOneDriveData(string DriveName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DuplexWCFfolderInfo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IFolderInfo
    {
        [OperationContract(IsOneWay = true)]
        void GetFolderInfo(string Path);
        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "DuplexWCFfolderInfo.ContractType".
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveFolderInfo(string[] DirectoryFiles);
    }
}

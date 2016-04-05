using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFdriversInfoServiceDLL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    interface IDiskInfo
    {
        [OperationContract]
        string GetDriversData();
        [OperationContract]
        MainDriveInfo GetOneDriveData(string DriveName);
        [OperationContract]
        List<string> GetDriversNames();
        //Этот метод записывает в лог файл имя клиента и дату подключения к WCF службе
        [OperationContract(IsOneWay = true)]
        void SaveDataInLog(string ClientName);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WCFdriversInfoServiceDLL.ContractType".
    [DataContract]
    public class MainDriveInfo
    {
        private string availableSpace;
        private string totalSpace;
        private string name;
        [DataMember]
        public string AvailableSpace
        {
            get
            {
                return availableSpace;
            }

            set
            {
                availableSpace = value;
            }
        }
        [DataMember]
        public string TotalSpace
        {
            get
            {
                return totalSpace;
            }

            set
            {
                totalSpace = value;
            }
        }
        [DataMember]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public MainDriveInfo(string NewAvailableSpace, string NewTotalSize, string NewName)
        {
            AvailableSpace = NewAvailableSpace;
            TotalSpace = NewTotalSize;
            Name = NewName;
        }
    }
}

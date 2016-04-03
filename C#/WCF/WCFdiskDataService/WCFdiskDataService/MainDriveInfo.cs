using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WCFdiskDataService
{
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

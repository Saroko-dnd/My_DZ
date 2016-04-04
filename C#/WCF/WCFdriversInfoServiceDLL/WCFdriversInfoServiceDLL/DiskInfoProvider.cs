using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFdriversInfoServiceDLL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class DiskInfoProvider : IDiskInfo
    {
        private readonly double GigabyteInBytes = 1073741824.0;

        public string GetDriversData()
        {
            StringBuilder BuilderForDriversData = new StringBuilder();
            try
            {
                DriveInfo[] AllDrivers = DriveInfo.GetDrives().Where(res => res.IsReady).ToArray();
                foreach (DriveInfo CurrentDriverInfo in AllDrivers)
                {
                    BuilderForDriversData.Append(MyResourses.Texts.DiskName + CurrentDriverInfo.Name + " " + MyResourses.Texts.FreeAvailable + " " + CurrentDriverInfo.AvailableFreeSpace.ToString() + " " +
                        MyResourses.Texts.Bytes + " " + MyResourses.Texts.From + " " + CurrentDriverInfo.TotalSize.ToString() + " " + MyResourses.Texts.Bytes);
                    BuilderForDriversData.AppendLine();
                }
                return BuilderForDriversData.ToString();
            }
            catch
            {
                return MyResourses.Texts.CantGetDriversInfo;
            }
        }

        public MainDriveInfo GetOneDriveData(string DriveName)
        {
            DriveInfo CurrentDriveInfo = DriveInfo.GetDrives().Where(res => res.IsReady == true && res.Name.Contains(DriveName)).FirstOrDefault();
            if (CurrentDriveInfo == null)
            {
                return null;
            }
            else
            {
                return new MainDriveInfo(((double)CurrentDriveInfo.AvailableFreeSpace / GigabyteInBytes).ToString(), ((double)CurrentDriveInfo.TotalSize / GigabyteInBytes).ToString(), CurrentDriveInfo.Name);
            }
        }

        public List<string> GetDriversNames()
        {
            return DriveInfo.GetDrives().Where(res => res.IsReady == true).Select(res => res.Name).ToList();
        }
    }
}

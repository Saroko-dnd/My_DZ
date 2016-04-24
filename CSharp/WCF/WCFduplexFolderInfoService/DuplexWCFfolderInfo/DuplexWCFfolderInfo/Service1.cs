using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Threading;

namespace DuplexWCFfolderInfo
{
    public class FolderInfoProvider : IFolderInfo
    {
        public void GetFolderInfo(string Path)
        {
            IClientCallback CurrentCallback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            ThreadPool.QueueUserWorkItem(o => ReturnFolderInfoToClient( Path, CurrentCallback));
        }

        public void ReturnFolderInfoToClient(string Path, IClientCallback CurrentCallback)
        {
            string[] ListOfFilesInDirectory = Directory.GetFiles(Path);
            CurrentCallback.ReceiveFolderInfo(ListOfFilesInDirectory);
        }

    }
}

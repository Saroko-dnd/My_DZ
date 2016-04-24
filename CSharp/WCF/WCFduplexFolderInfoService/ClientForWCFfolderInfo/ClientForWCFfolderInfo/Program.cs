using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ClientForWCFfolderInfo.FolderInfoService;

namespace ClientForWCFfolderInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MyResourses.Texts.PleaseGiveMePath);
            string CurrentPath = Console.ReadLine();
            CallbackHandler.Proxy.GetFolderInfo(CurrentPath);
            Console.ReadKey();
        }
    }

    public class CallbackHandler : IFolderInfoCallback
    {
        public static InstanceContext CurrentContext = new InstanceContext(new CallbackHandler());
        public static FolderInfoClient Proxy = new FolderInfoClient(CurrentContext);
        public void ReceiveFolderInfo(string[] DirectoryFiles)
        {
            foreach (string CurrentFile in DirectoryFiles)
            {
                Console.WriteLine(CurrentFile);
            }
        }
    }
}

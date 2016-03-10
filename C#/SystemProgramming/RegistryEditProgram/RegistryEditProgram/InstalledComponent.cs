using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryEditProgram
{
    public class InstalledComponent
    {
        private string ComponentName_;
        public string ComponentName
        {
            get { return ComponentName_; }
            set { ComponentName_ = value; }
        }
        private string ComponentPath_;
        public string ComponentPath
        {
            get { return ComponentPath_; }
            set { ComponentPath_ = value; }
        }

        public string UninstallPath = string.Empty;

        private string CanBeDeleted_ = string.Empty;
        public string CanBeDeleted
        {
            get { return CanBeDeleted_; }
            set { CanBeDeleted_ = value; }
        }

        public InstalledComponent(string NewName, string NewPath, string NewUninstallPath)
        {
            ComponentName = NewName;
            ComponentPath = NewPath;
            UninstallPath = NewUninstallPath;
            if (NewUninstallPath == string.Empty)
            {
                CanBeDeleted = MyResourses.FirstTabTexts.No;
            }
            else
            {
                CanBeDeleted = MyResourses.FirstTabTexts.Yes;
            }
        }

    }
}

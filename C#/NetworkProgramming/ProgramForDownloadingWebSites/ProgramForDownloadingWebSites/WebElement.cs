using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForDownloadingWebSites
{
    public class WebElement
    {
        public string ElementName = string.Empty;
        public string ReferencePartName = string.Empty;

        public WebElement(string NewName, string NewRerencefName)
        {
            ElementName = NewName;
            ReferencePartName = NewRerencefName;
        }
    }
}

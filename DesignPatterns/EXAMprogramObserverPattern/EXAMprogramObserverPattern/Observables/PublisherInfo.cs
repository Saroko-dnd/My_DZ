using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMprogramObserverPattern.Observables
{
    public class PublisherInfo
    {
        string lastNewspaper;

        public string LastNewspaper
        {
            get { return lastNewspaper; }
            set { lastNewspaper = value; }
        }

        string lastJournal;

        public string LastJournal
        {
            get { return lastJournal; }
            set { lastJournal = value; }
        }
    }
}

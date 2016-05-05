using EXAMprogramObserverPattern.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMprogramObserverPattern.Observables
{
    public class Publisher : AbstractObservable
    {
        bool ThisIsJournal = false;
        private string LastCreatedNewspaper = string.Empty;
        private string LastCreatedJournal = string.Empty;
        public void CreateNewNewspaper(string NewNameOfNewspaper)
        {
            LastCreatedNewspaper = NewNameOfNewspaper;
            ThisIsJournal = false;
            NotifyObservers();
        }

        public void CreateNewJournal(string NewNameOfJournal)
        {
            LastCreatedJournal = NewNameOfJournal;
            ThisIsJournal = true;
            NotifyObservers();
        }

        protected override void NotifyObservers()
        {
            PublisherInfo NewInfo;
            if (ThisIsJournal)
                NewInfo = new PublisherInfo() { LastJournal = LastCreatedJournal, LastNewspaper = string.Empty };
            else
                NewInfo = new PublisherInfo() { LastJournal = string.Empty, LastNewspaper = LastCreatedNewspaper };
            foreach (AbstractObserver CurrentObserver in ListOfObservers)
            {
                CurrentObserver.Update(NewInfo);
            }
        }
    }
}

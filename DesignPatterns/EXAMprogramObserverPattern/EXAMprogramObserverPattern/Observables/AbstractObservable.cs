using EXAMprogramObserverPattern.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMprogramObserverPattern.Observables
{
    public abstract class AbstractObservable
    {
        protected List<AbstractObserver> ListOfObservers = new List<AbstractObserver>();

        public void AddObserver(AbstractObserver NewObserver)
        {
            ListOfObservers.Add(NewObserver);
        }

        public void RemoveObserver(AbstractObserver CurrentObserver)
        {
            ListOfObservers.Remove(CurrentObserver);
        }

        protected abstract void NotifyObservers();
    }
}

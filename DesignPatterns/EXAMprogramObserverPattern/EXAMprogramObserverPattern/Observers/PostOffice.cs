using EXAMprogramObserverPattern.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EXAMprogramObserverPattern.Observers
{
    public class PostOffice : AbstractObservable, AbstractObserver
    {
        private TextBox JournalTextBox;
        private TextBox NewsPaperTextBox;

        PublisherInfo CurrentPublisherInfo = new PublisherInfo() { LastNewspaper = string.Empty, LastJournal = string.Empty };
        public void Update(Object NewData)
        {
            CurrentPublisherInfo = NewData as PublisherInfo;
            NewsPaperTextBox.Text = CurrentPublisherInfo.LastNewspaper;
            JournalTextBox.Text = CurrentPublisherInfo.LastJournal;
            NotifyObservers();
        }

        protected override void NotifyObservers()
        {
            foreach (AbstractObserver CurrentObserver in ListOfObservers)
            {
                CurrentObserver.Update(CurrentPublisherInfo);
            }
        }

        public PostOffice(TextBox NewJournalTextBox, TextBox NewNewsPaperTextBox)
        {
            JournalTextBox = NewJournalTextBox;
            NewsPaperTextBox = NewNewsPaperTextBox;
        }
    }
}

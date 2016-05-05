using EXAMprogramObserverPattern.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EXAMprogramObserverPattern.Observers
{
    public class Reader : AbstractObserver
    {
        TextBox AllNewspapersAndJournalsTextBox;
        StringBuilder BuilderForTextBox = new StringBuilder();
        private List<string> NecessaryEditions = new List<string>();
        private List<string> allNewspapersAndJournals = new List<string>();

        public List<string> AllNewspapersAndJournals
        {
            get { return allNewspapersAndJournals; }
            set { allNewspapersAndJournals = value; }
        }

        public void Update(Object NewData)
        {
            PublisherInfo CurrentInfo = NewData as PublisherInfo;
            if (NecessaryEditions.Where(res => res == CurrentInfo.LastJournal).FirstOrDefault() != null)
            {
                allNewspapersAndJournals.Add(CurrentInfo.LastJournal);
                BuilderForTextBox.Append(CurrentInfo.LastJournal);
                BuilderForTextBox.AppendLine();
                AllNewspapersAndJournalsTextBox.Text = BuilderForTextBox.ToString();
            }
            if (NecessaryEditions.Where(res => res == CurrentInfo.LastNewspaper).FirstOrDefault() != null)
            {
                allNewspapersAndJournals.Add(CurrentInfo.LastNewspaper);
                BuilderForTextBox.Append(CurrentInfo.LastNewspaper);
                BuilderForTextBox.AppendLine();
                AllNewspapersAndJournalsTextBox.Text = BuilderForTextBox.ToString();
            }
        }

        public Reader(List<string> NewNecessaryEditions, TextBox NewAllNewspapersAndJournalsTextBox)
        {
            AllNewspapersAndJournalsTextBox = NewAllNewspapersAndJournalsTextBox;
            NecessaryEditions = NewNecessaryEditions;
        }
    }
}

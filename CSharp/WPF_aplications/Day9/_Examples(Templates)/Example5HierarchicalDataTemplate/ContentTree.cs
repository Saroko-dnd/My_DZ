using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Example5HierarchicalDataTemplate
{
    public class ContentTree : INotifyPropertyChanged
    {
        string text;
        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged("Text"); }
        }

        string logo;
        public string Logo
        {
            get { return logo; }
            set { logo = value; OnPropertyChanged("Logo"); }
        }

        List<ContentTree> children;
        public List<ContentTree> Children
        {
            get { return children; }
            set
            {
                children = value;
                OnPropertyChanged("Children");
            }
        }

        public ContentTree()
        {
            children = new List<ContentTree>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler ph = this.PropertyChanged;

            if (ph != null)
                ph(this, new PropertyChangedEventArgs(name));
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgramForBookingWithoutBug
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private string testString;

        public string TestString
        {
            get
            {
                return testString;
            }

            set
            {
                testString = value;
                NotifyPropertyChanged();
            }
        }

        public BookingViewModel()
        {
            TestString = "Ненавижу";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}

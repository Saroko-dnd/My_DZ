using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicketBookingInterfaceTest.AllDictionaries
{
    public partial class ComboBoxDictionary
    {
        public void ComboBoxTextChanged(Object sender, EventArgs arguments)
        {
            string textd = (sender as ComboBox).Text;
            textd = (sender as ComboBox).Text;
        }
    }
}

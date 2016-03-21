using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatWithoutServer
{
    public static class CharsKiller
    {
        public static void InputValidationNames(object sender, TextCompositionEventArgs Event)
        {
            Regex CheckForCharsRegex = new Regex("#");
            if (CheckForCharsRegex.IsMatch(Event.Text))
                Event.Handled = true;
            else
                Event.Handled = false;
        }

        public static void InputValidationForIP(object sender, TextCompositionEventArgs Event)
        {
            Regex CheckForCharsRegex = new Regex("[0-9.]");
            if (CheckForCharsRegex.IsMatch(Event.Text))
                Event.Handled = false;
            else
                Event.Handled = true;
        }
    }
}

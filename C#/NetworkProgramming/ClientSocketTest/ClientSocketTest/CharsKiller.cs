using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ClientSocketTest
{
    public static class CharsKiller
    {
        public static void InputValidation(object sender, TextCompositionEventArgs Event)
        {
            Regex CheckForCharsRegex = new Regex("[0-9]");
            if (CheckForCharsRegex.IsMatch(Event.Text))
                Event.Handled = false;
            else
                Event.Handled = true;
        }

        public static void InputValidationForIP (object sender, TextCompositionEventArgs Event)
        {
            Regex CheckForCharsRegex = new Regex("[0-9.]");
            if (CheckForCharsRegex.IsMatch(Event.Text))
                Event.Handled = false;
            else
                Event.Handled = true;
        }
        public static void SpaceBarKillerPreviewKeyDown(object sender, KeyEventArgs Event)
        {
            if (Event.Key == Key.Space)
                Event.Handled = true;
            else
                Event.Handled = false;
        }
    }
}

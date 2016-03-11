using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace RegistryEditProgram
{
    public static class CharsKiller
    {
        //PreviwTextInput
        public static void InputValidation(object sender, TextCompositionEventArgs Event)
        {
            Regex CheckForCharsRegex = new Regex("[0-9]");
            if (CheckForCharsRegex.IsMatch(Event.Text))
                Event.Handled = false;
            else
                Event.Handled = true;
        }

        public static void InputValidationDoubleOnly(object sender, TextCompositionEventArgs Event)
        {
            Regex CheckForCharsRegex = new Regex("[0-9.,]");
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

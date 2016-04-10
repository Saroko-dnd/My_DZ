using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace DynamicLINQexample
{
    public static class CharsKiller
    {
        public static void OnlyNumbersPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex CheckForCharsRegex = new Regex(@"^\d*$");
            if (CheckForCharsRegex.IsMatch(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void OnlyDoublePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex CheckForCharsRegex = new Regex(@"^[\d,]*$");
            if (CheckForCharsRegex.IsMatch(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}

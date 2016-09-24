using SalaryGraphicsBuilder.DiagramCodeBehind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalaryGraphicsBuilder.EventsForMainWindowElements
{
    public static class MainWindowEventStorage
    {

        public static void TextBoxWithSalaryRangeTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox CurrentTextBox = sender as TextBox;
            try
            {
                int CurrentValueOfRangeFoSalaries = Int32.Parse(CurrentTextBox.Text);
                if (CurrentValueOfRangeFoSalaries > 0)
                {
                    DiagramManipulator.CurrentRangeForSalaries = CurrentValueOfRangeFoSalaries;
                }
                else
                {
                    DiagramManipulator.CurrentRangeForSalaries = DiagramManipulator.DefaultRangeForSalaries;
                }
            }
            catch
            {
                DiagramManipulator.CurrentRangeForSalaries = DiagramManipulator.DefaultRangeForSalaries;
            }

            if (CurrentTextBox.Text == string.Empty)
            {
                CurrentTextBox.Text = DiagramManipulator.DefaultRangeForSalaries.ToString();
            }
        }

        public static void TextBoxKeyPressDisableSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        public static void TextBoxTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            if ((sender as TextBox).Text == string.Empty && e.Text.StartsWith("0"))
            {
                e.Handled = true;
            }
            else
            {
                Regex RegexOnlyNumbers = new Regex("^[0-9]+$");
                if (!RegexOnlyNumbers.IsMatch(e.Text))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
        }

        public static void TextBoxPasteOnlyNumbers(object sender, DataObjectPastingEventArgs e)
        {
            //Check if buffer contain text now
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = e.DataObject.GetData(typeof(string)) as string;
                Regex RegexOnlyNumbers;
                if ((sender as TextBox).Text == string.Empty)
                {
                    RegexOnlyNumbers = new Regex("^[1-9][0-9]*$");
                }
                else
                {
                    RegexOnlyNumbers = new Regex("^[0-9]+$");
                }

                if (!RegexOnlyNumbers.IsMatch(pastedText))
                {
                    e.CancelCommand();
                }
            }
        }
    }
}

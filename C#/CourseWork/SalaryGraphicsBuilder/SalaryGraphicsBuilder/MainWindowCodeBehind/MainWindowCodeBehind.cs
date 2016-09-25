using SalaryGraphicsBuilder.DiagramCodeBehind;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalaryGraphicsBuilder.EventsForMainWindowElements
{
    public class MainWindowCodeBehind : INotifyPropertyChanged
    {   
        private static MainWindowCodeBehind SingleInstanceOfMainWindowCodeBehind = null;
        private static object GatesToInstance = new object();

        private Visibility visibilityForButtonForGettingInfoAboutSalaries = Visibility.Visible;
        private string titleForColumnChart = string.Empty;

        public Visibility VisibilityForButtonForGettingInfoAboutSalaries
        {
            get
            {
                return visibilityForButtonForGettingInfoAboutSalaries;
            }

            set
            {
                visibilityForButtonForGettingInfoAboutSalaries = value;
                NotifyPropertyChanged();
            }
        }

        public string TitleForColumnChart
        {
            get
            {
                return titleForColumnChart;
            }

            set
            {
                titleForColumnChart = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void TextBoxWithSalaryRangeTextChanged(object sender, TextChangedEventArgs e)
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

        public void TextBoxWithAmountOfRangesTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox CurrentTextBox = sender as TextBox;
            try
            {
                int CurrentAmountOfRangesFoSalaries = Int32.Parse(CurrentTextBox.Text);
                if (CurrentAmountOfRangesFoSalaries > 0)
                {
                    DiagramManipulator.CurrentAmountOfRanges = CurrentAmountOfRangesFoSalaries;
                }
                else
                {
                    DiagramManipulator.CurrentAmountOfRanges = DiagramManipulator.DefaultAmountOfRanges;
                }
            }
            catch
            {
                DiagramManipulator.CurrentAmountOfRanges = DiagramManipulator.DefaultAmountOfRanges;
            }

            if (CurrentTextBox.Text == string.Empty)
            {
                CurrentTextBox.Text = DiagramManipulator.DefaultAmountOfRanges.ToString();
            }
        }

        public void TextBoxKeyPressDisableSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        public void TextBoxTextInputOnlyNumbers(object sender, TextCompositionEventArgs e)
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

        public void TextBoxPasteOnlyNumbers(object sender, DataObjectPastingEventArgs e)
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

        public static MainWindowCodeBehind GetSingleInstanceOfMainWindowCodeBehind()
        {
            if (SingleInstanceOfMainWindowCodeBehind == null)
            {
                lock(GatesToInstance)
                {
                    if (SingleInstanceOfMainWindowCodeBehind == null)
                    {
                        SingleInstanceOfMainWindowCodeBehind = new MainWindowCodeBehind();

                    }
                    return SingleInstanceOfMainWindowCodeBehind;
                }
            }
            return SingleInstanceOfMainWindowCodeBehind;
        }
    }
}

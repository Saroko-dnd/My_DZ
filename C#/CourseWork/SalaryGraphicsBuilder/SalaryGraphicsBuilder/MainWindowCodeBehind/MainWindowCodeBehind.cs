using SalaryGraphicsBuilder.DiagramCodeBehind;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SalaryGraphicsBuilder.EventsForMainWindowElements
{
    public class MainWindowCodeBehind : INotifyPropertyChanged
    {   
        private static MainWindowCodeBehind SingleInstanceOfMainWindowCodeBehind = null;
        private static object GatesToInstance = new object();

        private Visibility visibilityForProgressBarForLoadingOfProfessions = Visibility.Collapsed;
        private Visibility visibilityForProgressBarInfoLabel = Visibility.Collapsed;
        private double valueForProgressBarForLoadingOfProfessions = 0.0;
        private double maximumForProgressBarForLoadingOfProfessions = 100.0;
        private double minimumForProgressBarForLoadingOfProfessions = 0.0;
        private string titleForColumnChart = string.Empty;
        private string contentForProgressBarInfoLabel = string.Empty;
        private bool gatherInfoAboutSalariesButtonIsEnabled = true;
        private bool uIControlsAreEnabled = false;
        private Brush textBoxForRangeValueInChart_Foreground = Brushes.Black;
        private Brush textBoxForAmountOfColumnsInChart_Foreground = Brushes.Black;

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

        public bool UIControlsAreEnabled
        {
            get
            {
                return uIControlsAreEnabled;
            }

            set
            {
                uIControlsAreEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public bool GatherInfoAboutSalariesButtonIsEnabled
        {
            get
            {
                return gatherInfoAboutSalariesButtonIsEnabled;
            }

            set
            {
                gatherInfoAboutSalariesButtonIsEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public Brush TextBoxForRangeValueInChart_Foreground
        {
            get
            {
                return textBoxForRangeValueInChart_Foreground;
            }

            set
            {
                textBoxForRangeValueInChart_Foreground = value;
                NotifyPropertyChanged();
            }
        }

        public Brush TextBoxForAmountOfColumnsInChart_Foreground
        {
            get
            {
                return textBoxForAmountOfColumnsInChart_Foreground;
            }

            set
            {
                textBoxForAmountOfColumnsInChart_Foreground = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility VisibilityForProgressBarForLoadingOfProfessions
        {
            get
            {
                return visibilityForProgressBarForLoadingOfProfessions;
            }

            set
            {
                visibilityForProgressBarForLoadingOfProfessions = value;
                NotifyPropertyChanged();
            }
        }

        public double ValueForProgressBarForLoadingOfProfessions
        {
            get
            {
                return valueForProgressBarForLoadingOfProfessions;
            }

            set
            {
                valueForProgressBarForLoadingOfProfessions = value;
                NotifyPropertyChanged();
            }
        }

        public double MaximumForProgressBarForLoadingOfProfessions
        {
            get
            {
                return maximumForProgressBarForLoadingOfProfessions;
            }

            set
            {
                maximumForProgressBarForLoadingOfProfessions = value;
                NotifyPropertyChanged();
            }
        }

        public double MinimumForProgressBarForLoadingOfProfessions
        {
            get
            {
                return minimumForProgressBarForLoadingOfProfessions;
            }

            set
            {
                minimumForProgressBarForLoadingOfProfessions = value;
                NotifyPropertyChanged();
            }
        }

        public string ContentForProgressBarInfoLabel
        {
            get
            {
                return contentForProgressBarInfoLabel;
            }

            set
            {
                contentForProgressBarInfoLabel = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility VisibilityForProgressBarInfoLabel
        {
            get
            {
                return visibilityForProgressBarInfoLabel;
            }

            set
            {
                visibilityForProgressBarInfoLabel = value;
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
            if (CurrentTextBox.Text.StartsWith("0"))
            {
                TextBoxForRangeValueInChart_Foreground = Brushes.Red;
            }
            else
            {
                TextBoxForRangeValueInChart_Foreground = Brushes.Black;
            }
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
            if (CurrentTextBox.Text.StartsWith("0"))
            {
                TextBoxForAmountOfColumnsInChart_Foreground = Brushes.Red;
            }
            else
            {
                TextBoxForAmountOfColumnsInChart_Foreground = Brushes.Black;
            }
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

        public void RefreshChartButtonClick(object sender, EventArgs e)
        {
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().GatherInfoAboutSalariesButtonIsEnabled = false;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().UIControlsAreEnabled = false;
            ThreadPool.QueueUserWorkItem(a => DiagramManipulator.CreateDataForDiagram(TitleForColumnChart));
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

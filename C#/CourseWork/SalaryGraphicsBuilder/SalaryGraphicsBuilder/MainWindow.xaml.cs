﻿using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using SalaryGraphicsBuilder.CodeOfExtractingData;
using SalaryGraphicsBuilder.DiagramCodeBehind;
using SalaryGraphicsBuilder.Resources;
using SalaryGraphicsBuilder.SerializationDeserializationXML;
using System.Collections.ObjectModel;
using SalaryGraphicsBuilder.EventsForMainWindowElements;

namespace SalaryGraphicsBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind();

            TextBoxForRangeValueInChart.PreviewTextInput += MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxTextInputOnlyNumbers;
            TextBoxForRangeValueInChart.PreviewKeyDown += MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxKeyPressDisableSpace;
            //Adding of handler for pasting in TextBox
            DataObject.AddPastingHandler(TextBoxForRangeValueInChart, MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxPasteOnlyNumbers);
            TextBoxForRangeValueInChart.TextChanged += MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxWithSalaryRangeTextChanged;

            TextBoxForAmountOfColumnsInChart.PreviewTextInput += MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxTextInputOnlyNumbers;
            TextBoxForAmountOfColumnsInChart.PreviewKeyDown += MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxKeyPressDisableSpace;
            DataObject.AddPastingHandler(TextBoxForAmountOfColumnsInChart, MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxPasteOnlyNumbers);
            TextBoxForAmountOfColumnsInChart.TextChanged += MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TextBoxWithAmountOfRangesTextChanged;

            ColumnDiagramForSalary.DataContext = DiagramManipulator.ValueListForWpfChart;
            ComboBoxForProfessions.DataContext = DataReceiver.ListOfProfessionNames;

            RefreshChartButton.Click += MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().RefreshChartButtonClick;
        }

        private void GetSalaryInfoButton_Click(object sender, RoutedEventArgs e)
        {
            string CurrentDirectoryPath = System.IO.Directory.GetCurrentDirectory();
            DataReceiver.PathToProfessionSalaryInfoFolder = CurrentDirectoryPath + "\\" + Texts.NameOfFolderForXMLfilesWithProfessionSalaryInfo;
            System.IO.Directory.CreateDirectory(DataReceiver.PathToProfessionSalaryInfoFolder);

            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().GatherInfoAboutSalariesButtonIsEnabled = false;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().UIControlsAreEnabled = false;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().VisibilityForProgressBarForLoadingOfProfessions = Visibility.Visible;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().ValueForProgressBarForLoadingOfProfessions = MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().MinimumForProgressBarForLoadingOfProfessions;
            ThreadPool.QueueUserWorkItem(a => DataReceiver.GetDataForSalaryGraphics());
        }

        private void ComboBoxForProfessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object CurrentSelectedProfession = (sender as ComboBox).SelectedItem;
            if (CurrentSelectedProfession != null)
            {
                string SelectedProfessionName = CurrentSelectedProfession.ToString();
                if (SelectedProfessionName != string.Empty)
                {
                    MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().GatherInfoAboutSalariesButtonIsEnabled = false;
                    MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().UIControlsAreEnabled = false;
                    MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().Text_TextBoxForAmountOfColumnsInChart = DiagramManipulator.CurrentAmountOfRanges.ToString();
                    MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().Text_TextBoxForRangeValueInChart = DiagramManipulator.CurrentRangeForSalaries.ToString();
                    ThreadPool.QueueUserWorkItem(a => DiagramManipulator.CreateDataForDiagram(SelectedProfessionName));
                }
            }
        }

        private void ButtonInfoAboutProgram_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Texts.MessageAboutProgram, Texts.CaptionAboutProgram, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}

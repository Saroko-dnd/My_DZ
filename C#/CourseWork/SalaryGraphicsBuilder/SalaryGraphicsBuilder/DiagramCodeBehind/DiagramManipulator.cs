﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.Controls;
using System.Collections.ObjectModel;
using SalaryGraphicsBuilder.CodeOfExtractingData;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using SalaryGraphicsBuilder.EventsForMainWindowElements;

namespace SalaryGraphicsBuilder.DiagramCodeBehind
{
    public static class DiagramManipulator
    {
        public static int DefaultRangeForSalaries = 200;
        public static int CurrentRangeForSalaries = 200;
        public static int DefaultAmountOfRanges = 10;
        public static int CurrentAmountOfRanges = 10;

        private static ObservableCollection<DynamicKeyValuePair> ValueListForWpfChart_ = new ObservableCollection<DynamicKeyValuePair>();

        public static ObservableCollection<DynamicKeyValuePair> ValueListForWpfChart
        {
            get { return DiagramManipulator.ValueListForWpfChart_; }
            private set { DiagramManipulator.ValueListForWpfChart_ = value; }
        }

        public static void CreateDataForDiagram(string CurrentProfessionName)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { ValueListForWpfChart.Clear(); }));

            Profession CurrentProfession = DataReceiver.ListOfInfoAboutProfessions.Where(ProfessionFound => ProfessionFound.ProfessionName == CurrentProfessionName).FirstOrDefault();
            if (CurrentProfession != null)
            {
                List<DynamicKeyValuePair> PureListOfValuesForWpfChart = new List<DynamicKeyValuePair>();
                int StartRangeValueForRange = 0;
                for (int Counter = 0; Counter < CurrentAmountOfRanges; ++Counter)
                {
                    if (Counter == (CurrentAmountOfRanges - 1))
                    {
                        PureListOfValuesForWpfChart.Add(new DynamicKeyValuePair(StartRangeValueForRange.ToString() + " and more", 0,
                            (StartRangeValueForRange + CurrentRangeForSalaries), StartRangeValueForRange));
                    }
                    else
                    {
                        PureListOfValuesForWpfChart.Add(new DynamicKeyValuePair(StartRangeValueForRange.ToString() + "-" + (StartRangeValueForRange + CurrentRangeForSalaries).ToString(), 0,
                            (StartRangeValueForRange + CurrentRangeForSalaries), StartRangeValueForRange));
                    }
                    StartRangeValueForRange += CurrentRangeForSalaries;
                }
                int MaxValueInsideChart = CurrentRangeForSalaries * CurrentAmountOfRanges;



                foreach (SalaryInfo CurrentSalaryInfo in CurrentProfession.ListOfInfoAboutOffers)
                {
                    if (CurrentSalaryInfo.Salary > MaxValueInsideChart)
                    {
                        PureListOfValuesForWpfChart.Last().Value += 1;
                    }
                    else
                    {
                        PureListOfValuesForWpfChart.Where(CurrentKeyValuePair =>
                            ((CurrentKeyValuePair.MinIntValue <= CurrentSalaryInfo.Salary) && (CurrentKeyValuePair.MaxIntValue >= CurrentSalaryInfo.Salary))).FirstOrDefault().Value += 1;
                    }
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    foreach (DynamicKeyValuePair CurrentKeyValuePair in PureListOfValuesForWpfChart)
                    {
                        ValueListForWpfChart.Add(CurrentKeyValuePair);
                    }
                }));

                MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().TitleForColumnChart = CurrentProfessionName;
            }

            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().GatherInfoAboutSalariesButtonIsEnabled = true;
            MainWindowCodeBehind.GetSingleInstanceOfMainWindowCodeBehind().UIControlsAreEnabled = true;
        } 
    }
}

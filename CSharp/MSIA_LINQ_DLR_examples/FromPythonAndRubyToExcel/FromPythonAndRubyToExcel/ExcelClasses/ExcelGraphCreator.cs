using FromPythonAndRubyToExcel.PythonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace FromPythonAndRubyToExcel.ExcelClasses
{
    public class ExcelGraphCreator
    {
        private static ExcelGraphCreator SingleGraphCreator;
        private static object GateObject = new object();

        public static ExcelGraphCreator GetExcelGraphCreator()
        {
            if (SingleGraphCreator == null)
            {
                lock (GateObject)
                {
                    if (SingleGraphCreator == null)
                    {
                        SingleGraphCreator = new ExcelGraphCreator();
                    }
                }
            }
            return SingleGraphCreator;
        }

        public void SaveDataInExcel(PythonWorker CurrentPythonWorker, string SavePath)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlApp.Visible = true;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "";
            dynamic DatesOfTests = CurrentPythonWorker.GetDatesOfTests();

            int RowCounter = 2;
            foreach (dynamic CurrentDate in DatesOfTests)
            {
                xlWorkSheet.Cells[RowCounter, 1] = CurrentDate;
                ++RowCounter;
            }

            dynamic TestsForSave = CurrentPythonWorker.GetListOfTests();
            int ColumnCounterForNamesOfTests = 2;
            foreach (dynamic CurrentTest in TestsForSave)
            {
                xlWorkSheet.Cells[1, ColumnCounterForNamesOfTests] = CurrentTest.GetTestName();
                dynamic CurrentTestResults = CurrentTest.GetValues();
                int RowCounterForTestValues = 2;
                foreach (dynamic CurrentValue in CurrentTestResults)
                {
                    xlWorkSheet.Cells[RowCounterForTestValues, ColumnCounterForNamesOfTests] = CurrentValue;
                    ++RowCounterForTestValues;
                }
                ++ColumnCounterForNamesOfTests;
            }

            Excel.Range chartRange;
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = xlCharts.Add(100, 200, 800, 350);
            Excel.Chart chartPage = myChart.Chart;

            chartRange = xlWorkSheet.get_Range((Excel.Range)xlWorkSheet.Cells[1, 1], (Excel.Range)xlWorkSheet.Cells[RowCounter - 1, ColumnCounterForNamesOfTests - 1]);
            chartPage.ChartWizard(Source: chartRange,
                Gallery: Excel.XlChartType.xlColumnClustered,
                Title: CurrentPythonWorker.GetLabName(),
                CategoryTitle: "Dates",
                ValueTitle: "Results of tests");

            if (SavePath == null || SavePath == string.Empty)
            {
                SavePath = Directory.GetCurrentDirectory() + MyResources.Texts.DefaultFileName;
            }

            xlWorkBook.SaveAs(SavePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show(MyResources.Texts.ExcelFileWasCreatedMessage + SavePath);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(MyResources.Texts.ReleaseObjectException + " " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private ExcelGraphCreator()
        {

        }

    }
}

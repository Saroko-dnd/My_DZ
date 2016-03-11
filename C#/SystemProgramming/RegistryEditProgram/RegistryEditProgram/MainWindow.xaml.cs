using System;
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
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace RegistryEditProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> AllBrushes = new List<string>();
        public static BrushConverter ConverterForConsoleBackground = new BrushConverter();

        public MainWindow()
        {
            InitializeComponent();
            //составляем список из Brushes для двух ComboBox
            foreach (PropertyInfo CurrentPropertyInfo in typeof(Brushes).GetProperties())
            {
                AllBrushes.Add(CurrentPropertyInfo.Name);
            }
            BackgroundBrushesComboBox.ItemsSource = AllBrushes;
            ForegroundBrushesComboBox.ItemsSource = AllBrushes;
            HeadersBrushesComboBox.ItemsSource = AllBrushes;

            UninstallProgramButton.Click += DeleteSelectedProgramButtonClick;
            GetSubKeysButton.Click += GetAllInstalledComponentsButton_Click;

            FontSizeTextBox.PreviewTextInput += CharsKiller.InputValidation;
            FontSizeTextBox.PreviewKeyDown += CharsKiller.SpaceBarKillerPreviewKeyDown;

            if (Registry.CurrentUser.OpenSubKey(MyResourses.FirstTabTexts.MyRegKey) == null)
            {
                try
                {
                    Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "ForegroundColor", @"Black");
                    Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "FontSize", 12);
                    Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "BacgroundColor", @"White");
                    Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "HeadersColor", @"White");
                }
                catch (Exception CurrentException)
                {
                    MessageBox.Show(CurrentException.Message,MyResourses.FirstTabTexts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            try
            {
                FontSizeTextBox.Text = ((int)Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "FontSize", 12)).ToString();
                BackgroundBrushesComboBox.SelectedItem = Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "BacgroundColor",
                    "White").ToString();
                ForegroundBrushesComboBox.SelectedItem = Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "ForegroundColor",
                    "Black").ToString();
                HeadersBrushesComboBox.SelectedItem = Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "HeadersColor",
                    "White").ToString();

                ProgramsDataGrid.FontSize = (int)Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "FontSize", 12);
                Style StyleForColumnHeaders = new Style();
                StyleForColumnHeaders.Setters.Add(new Setter(Control.BackgroundProperty, (Brush)ConverterForConsoleBackground.ConvertFromString(Registry.
                    GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "HeadersColor", "Black").ToString())));
                ProgramsDataGrid.ColumnHeaderStyle = StyleForColumnHeaders;
                ProgramsDataGrid.Foreground = (Brush)ConverterForConsoleBackground.ConvertFromString(Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "ForegroundColor", 
                    "Black").ToString());
                ProgramsDataGrid.RowBackground = (Brush)ConverterForConsoleBackground.ConvertFromString(Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "BacgroundColor", 
                    "White").ToString());

            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message, MyResourses.FirstTabTexts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void AddValueRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            //Добавление калькулятора в автозагрузку Windows 8
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Calculator", @"C:\Windows\System32\calc.exe");
        }
        
        private void GetAllInstalledComponentsButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> ListOfLocalMachine = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\").GetSubKeyNames().ToList();
            List<InstalledComponent> InstalledComponents = new List<InstalledComponent>();
            //выводим список установленных компонентов и програм
            foreach (string CurrentString in ListOfLocalMachine)
            {            
                InstalledComponents.Add
                    (
                    new InstalledComponent(
                    Registry.GetValue(MyResourses.FirstTabTexts.KeyToUninstall + CurrentString, "DisplayName", "Без имени").ToString(),
                    Registry.GetValue(MyResourses.FirstTabTexts.KeyToUninstall + CurrentString, "InstallLocation", "Не указан").ToString(),
                    Registry.GetValue(MyResourses.FirstTabTexts.KeyToUninstall + CurrentString, "UninstallString", string.Empty).ToString())
                    );
            }
            ProgramsDataGrid.ItemsSource = InstalledComponents;
        }

        private void DeleteSelectedProgramButtonClick(Object sender,EventArgs e)
        {
            if (ProgramsDataGrid.SelectedIndex >= 0)
            {
                if ((ProgramsDataGrid.SelectedItem as InstalledComponent).UninstallPath != string.Empty)
                {
                    string BufForUninstallPath = (ProgramsDataGrid.SelectedItem as InstalledComponent).UninstallPath.Replace("\"", "");
                    string BufForArguments = BufForUninstallPath.Replace(BufForUninstallPath.Split("/-".ToCharArray())[0], "");
                    Process.Start(new ProcessStartInfo() { FileName = BufForUninstallPath.Split("/-".ToCharArray())[0], Arguments = BufForArguments });
                }
                else
                {
                    MessageBox.Show(MyResourses.FirstTabTexts.CantFindUninstaller, MyResourses.FirstTabTexts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(MyResourses.FirstTabTexts.SelectedNothing, MyResourses.FirstTabTexts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SaveInRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            bool DataGridChanges = false;
            try
            {
                if (ProgramsDataGrid.FontSize != Int32.Parse(FontSizeTextBox.Text))
                {
                    ProgramsDataGrid.FontSize = Int32.Parse(FontSizeTextBox.Text);
                    Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "FontSize", Int32.Parse(FontSizeTextBox.Text));
                    DataGridChanges = true;
                }
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message + " " + MyResourses.FirstTabTexts.CheckDataFirst, MyResourses.FirstTabTexts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (ForegroundBrushesComboBox.SelectedIndex >= 0)
            {
                ProgramsDataGrid.Foreground = (Brush)ConverterForConsoleBackground.ConvertFromString(ForegroundBrushesComboBox.SelectedItem as string);
                Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "ForegroundColor", ForegroundBrushesComboBox.SelectedItem as string);
                DataGridChanges = true;
            }
            if (BackgroundBrushesComboBox.SelectedIndex >= 0)
            {
                ProgramsDataGrid.RowBackground = (Brush)ConverterForConsoleBackground.ConvertFromString(BackgroundBrushesComboBox.SelectedItem as string);
                Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "BacgroundColor", BackgroundBrushesComboBox.SelectedItem as string);
                DataGridChanges = true;
            }
            if (HeadersBrushesComboBox.SelectedIndex >= 0)
            {
                Style StyleForColumnHeaders = new Style();
                StyleForColumnHeaders.Setters.Add(new Setter(Control.BackgroundProperty, (Brush)ConverterForConsoleBackground.ConvertFromString(HeadersBrushesComboBox.SelectedItem as string)));
                ProgramsDataGrid.ColumnHeaderStyle = StyleForColumnHeaders;
                Registry.SetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "HeadersColor", HeadersBrushesComboBox.SelectedItem as string);
                DataGridChanges = true;
            }
            if (DataGridChanges)
            {
                MessageBox.Show(MyResourses.FirstTabTexts.ChangesDone, MyResourses.FirstTabTexts.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show(MyResourses.FirstTabTexts.ChangesNotDone, MyResourses.FirstTabTexts.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }           
        }
    }
}

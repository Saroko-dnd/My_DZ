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

namespace RegistryEditProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> AllBrushes = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            ProgramsDataGrid.ItemsSource = typeof(Brushes).GetProperties().Select(res => new { Name = res.Name }).ToList();
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
                }
                catch (Exception CurrentException)
                {
                    MessageBox.Show(CurrentException.Message,MyResourses.FirstTabTexts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            try
            {
                FontSizeTextBox.Text = ((int)Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "FontSize", 12)).ToString();
                ProgramsDataGrid.FontSize = (int)Registry.GetValue(MyResourses.FirstTabTexts.MyRegKeyFull, "FontSize", 12);
                BrushConverter ConverterForConsoleBackground = new BrushConverter();
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
    }
}

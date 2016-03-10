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
        public MainWindow()
        {
            InitializeComponent();
            
            MessageBox.Show(Brushes.Purple.Color.ToString());
            UninstallProgramButton.Click += DeleteSelectedProgramButtonClick;
            GetSubKeysButton.Click += GetAllInstalledComponentsButton_Click;
            //Registry.SetValue(@"HKEY_CURRENT_USER\RegC#Test", "ForegroundColor", @"Red");
            //Registry.SetValue(@"HKEY_CURRENT_USER\RegC#Test", "FontSize", 12);
            ProgramsDataGrid.FontSize = (int)Registry.GetValue(@"HKEY_CURRENT_USER\RegC#Test", "FontSize", 0);           
            BrushConverter ConverterForConsoleBackground = new BrushConverter();
            ProgramsDataGrid.Foreground = (Brush)ConverterForConsoleBackground.ConvertFromString(Registry.GetValue(@"HKEY_CURRENT_USER\RegC#Test", "ForegroundColor", "Black").ToString());
            ProgramsDataGrid.RowBackground = (Brush)ConverterForConsoleBackground.ConvertFromString(Registry.GetValue(@"HKEY_CURRENT_USER\RegC#Test", "BacgroundColor", "White").ToString());
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
            if (ProgramsDataGrid.SelectedIndex >= 0 && (ProgramsDataGrid.SelectedItem as InstalledComponent).UninstallPath != string.Empty)
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

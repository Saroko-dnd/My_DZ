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
            GetSubKeysButton.Click += GetAllInstalledComponentsButton_Click;
            /*Registry.SetValue(@"HKEY_CURRENT_USER\RegC#Test", "BacgroundColor", @"Yellow");
            Registry.SetValue(@"HKEY_CURRENT_USER\RegC#Test", "FontSize", 12);*/
            ConsoleTextBox.FontSize = (int)Registry.GetValue(@"HKEY_CURRENT_USER\RegC#Test", "FontSize", 0);           
            BrushConverter ConverterForConsoleBackground = new BrushConverter();
            ConsoleTextBox.Background = (Brush) ConverterForConsoleBackground.ConvertFromString(Registry.GetValue(@"HKEY_CURRENT_USER\RegC#Test", "BacgroundColor", "empty").ToString());
        }

        private void AddValueRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            //Добавление калькулятора в автозагрузку Windows 8
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "Calculator", @"C:\Windows\System32\calc.exe");
        }
        
        private void GetAllInstalledComponentsButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder BuilderForConsole = new StringBuilder();
            List<string> ListOfLocalMachine = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\").GetSubKeyNames().ToList();
            //выводим список установленных компонентов и програм
            foreach (string CurrentString in ListOfLocalMachine)
            {
                BuilderForConsole.AppendLine(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + CurrentString, "DisplayName", "Без имени").ToString() + " " +
                    "Path: " + Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + CurrentString, "InstallLocation", "Не указан").ToString());
            }
            ConsoleTextBox.Text = BuilderForConsole.ToString();
        }
    }
}

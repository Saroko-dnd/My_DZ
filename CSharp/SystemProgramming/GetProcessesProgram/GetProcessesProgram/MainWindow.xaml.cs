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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace GetProcessesProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDomain d = AppDomain.CreateDomain("Domain");
            AssemblyName an = new AssemblyName("Assembly");
            d.ExecuteAssembly(an, null, null);
            ProcessesDataGrid.ItemsSource = Process.GetProcessesByName("devenv").Select(res => new { res.ProcessName, res.Id });
            //Возвращает нити одного процесса
            ThreadsDataGrid.ItemsSource = Process.GetProcessesByName("devenv")[0].Threads;
            //Возращае dll необходимые для работы процесса
            dllDataGrid.ItemsSource = Process.GetProcessesByName("devenv")[0].Modules;
            //Запуск приложения (пример)
            //Process.Start(new ProcessStartInfo() { FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", Arguments = @"https://en.wikipedia.org/wiki/CPU_time" });
            //Возвращает все сборки одного домена
            DomainInfoGrid.ItemsSource = AppDomain.CurrentDomain.GetAssemblies();
            //Creating of new domain
            //****************************************************************
            AppDomain NewDomain = AppDomain.CreateDomain("CalculationOfFactorial");
            NewDomain.AssemblyLoad += DomainAssemblyLoad;
            NewDomain.DomainUnload += DomainAssemblyUnLoad;
            int Number = 5;
            string[] args = new string[] { Number.ToString() };
            //Path to another exe file from current exe
            string PathToAssembly = NewDomain.BaseDirectory +
                "../../../../CalculationOfFactorial/CalculationOfFactorial/bin/Debug/CalculationOfFactorial.exe";
            //THis is the only way to dinamically add and remove libraries
            //Because we couldnt do that in THIS domain only in "NewDomain"
            NewDomain.Load(new AssemblyName("System.Data"));
            //We run here our another application
            NewDomain.ExecuteAssembly(PathToAssembly, args);
            DomainInfoGrid.ItemsSource = NewDomain.GetAssemblies();
            AppDomain.Unload(NewDomain);
            //******************************************************************
        }

    public static void DomainAssemblyLoad(Object sender, EventArgs Arguments)
        {
            MessageBox.Show("Assembly LOAD");
        }

        public static void DomainAssemblyUnLoad(Object sender, EventArgs Arguments)
        {
            MessageBox.Show("Assembly UNLOAD");
        }
    }
}

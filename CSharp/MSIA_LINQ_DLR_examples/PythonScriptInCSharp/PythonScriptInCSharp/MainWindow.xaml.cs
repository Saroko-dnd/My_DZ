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
using IronPython.Runtime;
using IronPython;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace PythonScriptInCSharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            //Создаем Runtime для Python
            ScriptEngine PythonEngine = Python.CreateEngine();
            //Загружаем Python скрипт
            dynamic PythonScript = PythonEngine.ExecuteFile("Customer.py.txt");
            //Получаем объект класса описанного в скрипте 
            dynamic customer = PythonScript.GetNewCustomer(99, "Fred", "111");
            //Получение строки из скрипта
            PythonTextBox.Text = PythonScript.PrintHelloFromPython();
        }
    }
}

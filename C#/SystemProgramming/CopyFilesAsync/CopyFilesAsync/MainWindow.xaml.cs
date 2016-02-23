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
using System.IO;
//for open dialog
using Microsoft.Win32;
using System.Threading;

namespace CopyFilesAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static OpenFileDialog FileSelectionDialogFrom = new OpenFileDialog();
        public static OpenFileDialog FileSelectionDialogWhere = new OpenFileDialog();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CopyFileAsyncFunction()
        {
            
        }

        private void WhereButton_Click(object sender, RoutedEventArgs e)
        {
            FileSelectionDialogWhere.ShowDialog();
        }

        private void FromButton_Click(object sender, RoutedEventArgs e)
        {
            FileSelectionDialogFrom.ShowDialog();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Thread FileCopyThread = new Thread(CopyFile);
            FileCopyThread.Start();
        }

        public void CopyFile()
        {
            
            File.Copy(FileSelectionDialogFrom.FileName, FileSelectionDialogWhere.FileName);
        }
    }
}

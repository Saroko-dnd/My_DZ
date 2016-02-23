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
        public static SaveFileDialog FileDialogWhere = new SaveFileDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void CopyFileAsyncFunction()
        {
            
        }

        private void WhereButton_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWhere.ShowDialog();
            WhereButton.Content = FileDialogWhere.FileName;
        }

        private void FromButton_Click(object sender, RoutedEventArgs e)
        {
            FileSelectionDialogFrom.ShowDialog();
            FromButton.Content = FileSelectionDialogFrom.FileName;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Thread FileCopyThread = new Thread(() => CopyFile());
            FileCopyThread.Start();
        }

        public void CopyFile()
        {
            using (FileStream source = new FileStream(FileSelectionDialogFrom.FileName, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[4000];
                long fileLength = source.Length;
                using (FileStream dest = new FileStream(FileDialogWhere.FileName, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;
                        dest.Write(buffer, 0, currentBlockSize);
                        Dispatcher.Invoke(
                                new System.Action(() => CopyProgressBar.Value = persentage)
                                );
                    }
                }
            }
        }
    }
}

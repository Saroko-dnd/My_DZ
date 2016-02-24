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
        public bool MainPause = false;
        public bool MainBreak = false;

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
                bool pause = false;
                bool BreakNow = false;
                byte[] buffer = new byte[4000];
                long fileLength = source.Length;
                using (FileStream dest = new FileStream(FileDialogWhere.FileName, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0 )
                    {
                        do
                        {
                            Dispatcher.Invoke(
                            new System.Action(() => BreakNow = MainBreak)
                            );
                            if (BreakNow)
                                break;
                            Dispatcher.Invoke(
                            new System.Action(() => pause = MainPause)
                            );
                        } while (pause && !BreakNow);
                        if (BreakNow)
                        {
                            break;
                        }
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;
                        dest.Write(buffer, 0, currentBlockSize);
                        Dispatcher.Invoke(
                                new System.Action(() => CopyProgressBar.Value = persentage)
                                );
                    }
                    if (BreakNow)
                        Dispatcher.Invoke(
                            new System.Action(() => MainBreak = false)
                            );
                    else
                    {
                        Dispatcher.Invoke(
                        new System.Action(() => CopyProgressBar.Value = 0.0)
                        );
                        Dispatcher.Invoke(
                        new System.Action(() => CopyStateLabel.Content = MyResourses.Texts.Done)
                        );
                    }

                }
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MainPause = true;
            CopyStateLabel.Content = MyResourses.Texts.PauseNow;
        }

        private void UnPauseButton_Click(object sender, RoutedEventArgs e)
        {
            MainPause = false;
            CopyStateLabel.Content = string.Empty;
        }

        private void BreakButtonClick(object sender, RoutedEventArgs e)
        {
            MainBreak = true;
            CopyStateLabel.Content = MyResourses.Texts.BreakNow;
        }
    }
}

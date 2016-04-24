using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CopyFilesAsync
{
    static class AsyncCopyFiles
    {
        public static OpenFileDialog FileSelectionDialogFrom = new OpenFileDialog();
        public static SaveFileDialog FileDialogWhere = new SaveFileDialog();
        public static bool MainPause = false;
        public static bool MainBreak = false;
        public static ProgressBar CopyProgressBar = new ProgressBar();
        public static Label CopyStateLabel = new Label();

        public static byte[] StaticBuffer = new byte[4000];
        public static string StaticBufferState = MyResourses.Texts.Empty;
        public static int AmountOfBytes = 0;
        public static long SourceLength = 0;

        //Обработчики нажатий клавиш*************************************************
        public static void FromButton_Click(object sender, RoutedEventArgs e)
        {
            FileSelectionDialogFrom.ShowDialog();
            (sender as Button).Content = FileSelectionDialogFrom.FileName;
        }

        public static void WhereButton_Click(object sender, RoutedEventArgs e)
        {
            FileDialogWhere.ShowDialog();
            (sender as Button).Content = FileDialogWhere.FileName;
        }

        public static void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Thread FileReadThread = new Thread(() => CopyFileReadFunction());
            Thread FileWriteThread = new Thread(() => CopyFileWriteFunction());
            FileReadThread.Start();
            FileWriteThread.Start();
        }

        public static void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MainPause = true;
            CopyStateLabel.Content = MyResourses.Texts.PauseNow;
        }

        public static void UnPauseButton_Click(object sender, RoutedEventArgs e)
        {
            MainPause = false;
            CopyStateLabel.Content = string.Empty;
        }

        public static void BreakButtonClick(object sender, RoutedEventArgs e)
        {
            MainBreak = true;
            CopyStateLabel.Content = MyResourses.Texts.BreakNow;
        }
        //*******************************************************************************
        public static void CopyFileReadFunction()
        {
            using (FileStream source = new FileStream(FileSelectionDialogFrom.FileName, FileMode.Open, FileAccess.Read))
            {
                string CurrentBufferState = MyResourses.Texts.Empty;
                bool pause = false;
                bool BreakNow = false;
                byte[] buffer = new byte[4000];
                int currentBlockSize = 0;
                Application.Current.Dispatcher.Invoke(
                    new System.Action(() => SourceLength = source.Length)
                    );
                while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    do
                    {
                        Application.Current.Dispatcher.Invoke(
                        new System.Action(() => BreakNow = MainBreak)
                        );
                        Application.Current.Dispatcher.Invoke(
                        new System.Action(() => pause = MainPause)
                        );
                    } while (pause && !BreakNow);
                    if (BreakNow)
                    {
                        break;
                    }
                    do
                    {
                        Application.Current.Dispatcher.Invoke(
                            new System.Action(() => CurrentBufferState = StaticBufferState)
                            );
                    } while (CurrentBufferState != MyResourses.Texts.Empty);
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => AmountOfBytes = currentBlockSize)
                        );
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => StaticBuffer = buffer.Clone() as byte[])
                        );
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => StaticBufferState = MyResourses.Texts.Full)
                        );
                }
                Application.Current.Dispatcher.Invoke(
                    new System.Action(() => StaticBufferState = MyResourses.Texts.EmptyForever)
                    );
                if (BreakNow)
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => MainBreak = false)
                        );
            }
        }

            public static void CopyFileWriteFunction()
            {
                string CurrentBufferState = MyResourses.Texts.Empty;
                byte[] buffer = new byte[4000];
                long fileLength = 0;
                long totalBytes = 0;
                int currentBlockSize = 0;
                using (FileStream dest = new FileStream(FileDialogWhere.FileName, FileMode.CreateNew, FileAccess.Write))
                {
                    while (true)
                    {
                        do
                        {
                            Application.Current.Dispatcher.Invoke(
                                new System.Action(() => CurrentBufferState = StaticBufferState)
                                );
                        } while (CurrentBufferState == MyResourses.Texts.Empty);
                        if (fileLength == 0)
                        {
                            Application.Current.Dispatcher.Invoke(
                                new System.Action(() => fileLength = SourceLength)
                                );
                        }
                        if (CurrentBufferState == MyResourses.Texts.EmptyForever)
                        {
                            break;
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(
                                new System.Action(() => buffer = StaticBuffer.Clone() as byte[])
                                );
                            Application.Current.Dispatcher.Invoke(
                                new System.Action(() => currentBlockSize = AmountOfBytes)
                                );
                            Application.Current.Dispatcher.Invoke(
                                new System.Action(() => CurrentBufferState = StaticBufferState)
                                );
                            if (CurrentBufferState != MyResourses.Texts.EmptyForever)
                            {
                                Application.Current.Dispatcher.Invoke(
                                        new System.Action(() => StaticBufferState = MyResourses.Texts.Empty)
                                        );
                            }
                            totalBytes += currentBlockSize;
                            double persentage = (double)totalBytes * 100.0 / fileLength;
                            dest.Write(buffer, 0, currentBlockSize);
                            Application.Current.Dispatcher.Invoke(
                                new System.Action(() => CopyProgressBar.Value = persentage)
                                );
                        }
                    }
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => CopyProgressBar.Value = 0.0)
                        );
                    Application.Current.Dispatcher.Invoke(
                        new System.Action(() => CopyStateLabel.Content = MyResourses.Texts.Done)
                        );
            }
            }
    }
}


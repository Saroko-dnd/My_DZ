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
        public static byte[] Buffer_1 = new byte[4000];
        public static byte[] Buffer_2 = new byte[4000];
        public static string Buffer_1_State = "";
        public static string Buffer_2_State = "";
        public static bool FirstBuffer = true;


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
            Thread FileCopyThread_1 = new Thread(() => CopyFileThreadFunction_1());

            FileCopyThread_1.Start();

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
        public static void CopyFileThreadFunction_1()
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


                    while ((currentBlockSize = FirstBuffer ? source.Read(Buffer_1, 0, Buffer_1.Length) : 
                        source.Read(Buffer_2, 0, Buffer_2.Length)) > 0)                          
                    {
                        do
                        {
                            Application.Current.Dispatcher.Invoke(
                            new System.Action(() => BreakNow = MainBreak)
                            );
                            if (BreakNow)
                                break;
                            Application.Current.Dispatcher.Invoke(
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
                        Application.Current.Dispatcher.Invoke(
                                new System.Action(() => CopyProgressBar.Value = persentage)
                                );
                    }
                    if (BreakNow)
                        Application.Current.Dispatcher.Invoke(
                            new System.Action(() => MainBreak = false)
                            );
                    else
                    {
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

        public static void CopyFileThreadFunction_2()
        {
            using (FileStream dest = new FileStream(FileDialogWhere.FileName, FileMode.CreateNew, FileAccess.Write))
            {
                
            }
        }
    }
}


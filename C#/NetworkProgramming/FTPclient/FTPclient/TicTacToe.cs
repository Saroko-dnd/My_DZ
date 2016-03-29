using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.Windows;

namespace FTPclient
{
    public static class TicTacToe
    {
        public static ComboBox SelectorOfChars = null;
        public static Label GameStatusLabel = null;
        public static List<char> GameSymbols = new List<char>() {'0','X' };
        public static bool FirstTime = true;
        public static bool WaitForAnswer = false;
        public static int NumberOfLastPressedButton = -1;

        public static void CheckWin()
        {
            string SelectedChar = string.Empty;
            Application.Current.Dispatcher.Invoke(new Action(() => SelectedChar = SelectorOfChars.SelectedItem.ToString()));
            if (MainWindow.GameFieldButtons[0].Content ==  MainWindow.GameFieldButtons[4].Content && MainWindow.GameFieldButtons[0].Content == MainWindow.GameFieldButtons[8].Content)
            {
                if (SelectedChar == MainWindow.GameFieldButtons[0].Content)
                {
                    MessageBox.Show(MyResourses.Texts.YouWin, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show(MyResourses.Texts.YouLose, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                StartNewGame();
            }
            else if (MainWindow.GameFieldButtons[2].Content == MainWindow.GameFieldButtons[4].Content && MainWindow.GameFieldButtons[2].Content == MainWindow.GameFieldButtons[6].Content)
            {
                if (SelectedChar == MainWindow.GameFieldButtons[2].Content)
                {
                    MessageBox.Show(MyResourses.Texts.YouWin, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show(MyResourses.Texts.YouLose, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                StartNewGame();
            }
            else
            {
                for (int counter = 0; counter < 3; ++counter)
                {
                    if (MainWindow.GameFieldButtons[counter].Content == MainWindow.GameFieldButtons[counter + 3].Content && MainWindow.GameFieldButtons[counter].Content == MainWindow.GameFieldButtons[counter + 6].Content)
                    {
                        if (SelectedChar == MainWindow.GameFieldButtons[counter].Content)
                        {
                            MessageBox.Show(MyResourses.Texts.YouWin, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show(MyResourses.Texts.YouLose, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        StartNewGame();
                        return;
                    }                 
                }
                for (int counter = 0; counter < 7; counter += 3)
                {
                    if (MainWindow.GameFieldButtons[counter].Content == MainWindow.GameFieldButtons[counter + 1].Content && MainWindow.GameFieldButtons[counter].Content == MainWindow.GameFieldButtons[counter + 2].Content)
                    {
                        if (SelectedChar == MainWindow.GameFieldButtons[counter].Content)
                        {
                            MessageBox.Show(MyResourses.Texts.YouWin, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show(MyResourses.Texts.YouLose, MyResourses.Texts.Result, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        StartNewGame();
                        return;
                    }
                }
            }
        }

        public static void StartNewGame()
        {
            string DataForFtp ="new"  + "\r\n";
            string FileName = "ftp://" + MainWindow.GetLocalIPAddress() + "/GameInfo.txt";
            FtpWebRequest request = null;
            Stream requestStream = null;
            bool done = false;
            while (!done)
            {
                try
                {
                    request = (FtpWebRequest)WebRequest.Create(FileName);
                    request.KeepAlive = false;
                    request.Timeout = 10000;
                    request.Method = WebRequestMethods.Ftp.AppendFile;
                    request.ContentLength = Encoding.ASCII.GetBytes(DataForFtp).Length;
                    requestStream = request.GetRequestStream();
                    done = true;
                }
                catch (Exception CurrentException)
                {
                    if (requestStream != null)
                    {
                        requestStream.Close();
                        requestStream = null;
                    }
                    if (!CurrentException.Message.Contains("451"))
                    {
                        MessageBox.Show(MyResourses.Texts.CantAppendFile + CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        GameStatusLabel.Content = MyResourses.Texts.GameStatusNone;
                        return;
                    }
                }
            }

            requestStream.Write(Encoding.ASCII.GetBytes(DataForFtp), 0, Encoding.ASCII.GetBytes(DataForFtp).Length);
            requestStream.Close();
            FtpWebResponse response = null;
            bool FileUpdated = false;
            while (!FileUpdated)
            {
                try
                {
                    GameStatusLabel.Content = MyResourses.Texts.GameStatusWait;
                    response = (FtpWebResponse)request.GetResponse();
                    GameStatusLabel.Content = MyResourses.Texts.GameStatusNone;
                    FileUpdated = true;
                }
                catch (Exception CurrentException)
                {
                    //игнорируем использование файла другим процессом
                    if (!CurrentException.Message.Contains("451"))
                    {
                        MessageBox.Show(MyResourses.Texts.CantAppendFile, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        GameStatusLabel.Content = MyResourses.Texts.GameStatusNone;
                        break;
                    }
                }
            }
        }

        public static void Play(object sender, EventArgs e)
        {
            if ((sender as Button).Content != "X" && (sender as Button).Content != "0")
            {
                if (WaitForAnswer)
                {
                    MessageBox.Show(MyResourses.Texts.WaitForAnswerError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int SelectedCharIndex = -1;
                    Application.Current.Dispatcher.Invoke(new Action(() => SelectedCharIndex = SelectorOfChars.SelectedIndex));
                    if (SelectedCharIndex >= 0)
                    {
                        string SelectedGameChar = string.Empty;
                        Application.Current.Dispatcher.Invoke(new Action(() => SelectedGameChar = SelectorOfChars.SelectedItem.ToString()));
                        string DataForFtp = (sender as Button).Name.Split('_')[1] + " " + SelectedGameChar + "\r\n";
                        string FileName = "ftp://" + MainWindow.GetLocalIPAddress() + "/GameInfo.txt";

                        FtpWebRequest request = null;
                        Stream requestStream = null;
                        bool done = false;
                        while (!done)
                        {
                            try
                            {
                                request = (FtpWebRequest)WebRequest.Create(FileName);
                                request.KeepAlive = false;
                                request.Timeout = 10000;
                                request.Method = WebRequestMethods.Ftp.AppendFile;
                                request.ContentLength = Encoding.ASCII.GetBytes(DataForFtp).Length;
                                requestStream = request.GetRequestStream();
                                done = true;
                            }
                            catch (Exception CurrentException)
                            {
                                if (requestStream != null)
                                {
                                    requestStream.Close();
                                    requestStream = null;
                                }
                                if (!CurrentException.Message.Contains("451"))
                                {
                                    MessageBox.Show(MyResourses.Texts.CantAppendFile + CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                                    GameStatusLabel.Content = MyResourses.Texts.GameStatusNone;                                   
                                    return;
                                }
                            }
                        }

                        requestStream.Write(Encoding.ASCII.GetBytes(DataForFtp), 0, Encoding.ASCII.GetBytes(DataForFtp).Length);
                        requestStream.Close();
                        FtpWebResponse response = null;
                        bool FileUpdated = false;
                        while (!FileUpdated)
                        {
                            try
                            {
                                GameStatusLabel.Content = MyResourses.Texts.GameStatusWait;
                                response = (FtpWebResponse)request.GetResponse();
                                (sender as Button).Content = SelectedGameChar;
                                GameStatusLabel.Content = MyResourses.Texts.GameStatusDataWasSend;
                                if (FirstTime)
                                {
                                    FirstTime = false;
                                    if (SelectedGameChar == "X")
                                    {
                                        ThreadPool.QueueUserWorkItem(o => MainWindow.StartGetDataFromFTPserver("0"));
                                    }
                                    else
                                    {
                                        ThreadPool.QueueUserWorkItem(o => MainWindow.StartGetDataFromFTPserver("X"));
                                    }

                                }
                                NumberOfLastPressedButton = Int32.Parse((sender as Button).Name.Split('_')[1]);
                                WaitForAnswer = true;
                                FileUpdated = true;
                            }
                            catch (Exception CurrentException)
                            {
                                //игнорируем использование файла другим процессом
                                if (!CurrentException.Message.Contains("451"))
                                {
                                    MessageBox.Show(MyResourses.Texts.CantAppendFile, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                                    GameStatusLabel.Content = MyResourses.Texts.GameStatusNone;
                                    break;
                                }
                            }
                        }
                        if (response != null)
                        {
                            response.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(MyResourses.Texts.YouMustSelectChar, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }

    }
}

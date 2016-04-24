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
using System.Net;
using System.Net.Mail;
using Microsoft.Win32;
using System.Threading;

namespace EmailSimpleClientProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> FileNamesForAttachments = new List<string>();
        public bool ProgramShutDown = false;
        public bool ProgramBusy = false;

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += ShutDown;
            ThreadPool.SetMinThreads(4, 4);
            AttachmentsListBox.ItemsSource = FileNamesForAttachments;
        }

        public void ShutDown(object sender, EventArgs e)
        {
            ProgramShutDown = true;
        }

        public void SendMessage(string Subject ,string Message ,string FromAddress, string ToAddress, string SMTPserver, string Password)
        {
            using (SmtpClient NewSmtpClient = new SmtpClient())
            {
                using (MailMessage NewEmail = new MailMessage())
                {
                    try
                    {
                        NewEmail.BodyEncoding = Encoding.UTF8;
                        NewEmail.Body = Message;
                        NewEmail.From = new MailAddress(FromAddress);
                        NewEmail.To.Add(new MailAddress(ToAddress));
                        NewEmail.Subject = Subject;

                        foreach (string FileName in FileNamesForAttachments)
                        {
                            NewEmail.Attachments.Add(new Attachment(FileName));
                        }

                        NewSmtpClient.UseDefaultCredentials = false;
                        NewSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        NewSmtpClient.EnableSsl = true;
                        NewSmtpClient.Port = 587;
                        NewSmtpClient.Host = SMTPserver;
                        NewSmtpClient.Credentials = new NetworkCredential(FromAddress, Password);

                        NewSmtpClient.Send(NewEmail);
                    }
                    catch (Exception CurrentException)
                    {
                        if (!ProgramShutDown)
                        {
                            MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    finally
                    {
                        ProgramBusy = false;
                        Application.Current.Dispatcher.Invoke(new Action(() => StatusLabel.Content = MyResourses.Texts.StatusFree));
                    }
                }
            }
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProgramBusy)
                {
                    MessageBox.Show(MyResourses.Texts.CantSendTwoEmail, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ToAddressTextBox.Text == string.Empty || FromAddressTextBox.Text == string.Empty || SMTPserverTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty)
                {
                    MessageBox.Show(MyResourses.Texts.ParameterEmptyError, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ProgramBusy = true;
                    StatusLabel.Content = MyResourses.Texts.StatusBusy;
                    string Message = MessageTextBox.Text;
                    string FromAddress = FromAddressTextBox.Text;
                    string ToAddress = ToAddressTextBox.Text;
                    string SMTPserver = SMTPserverTextBox.Text;
                    string Password = PasswordTextBox.Text;
                    string Subject = SubjectTextBox.Text;
                    ThreadPool.QueueUserWorkItem(o => SendMessage(Subject, Message, FromAddress, ToAddress, SMTPserver, Password));
                }
                 
            }
            catch (Exception CurrentException)
            {
                MessageBox.Show(CurrentException.Message, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddAttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog FileDialogForAttachments = new OpenFileDialog();
            if (FileDialogForAttachments.ShowDialog() == true)
            {
                FileNamesForAttachments.Add(FileDialogForAttachments.FileName);
                AttachmentsListBox.Items.Refresh();
            }
        }

        private void RemoveAttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AttachmentsListBox.SelectedIndex >= 0)
            {
                FileNamesForAttachments.Remove(AttachmentsListBox.SelectedItem as string);
                AttachmentsListBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show(MyResourses.Texts.SelectedNone, MyResourses.Texts.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveAllAttachmentsButton_Click(object sender, RoutedEventArgs e)
        {
            FileNamesForAttachments.Clear();
            AttachmentsListBox.Items.Refresh();
        }
    }
}

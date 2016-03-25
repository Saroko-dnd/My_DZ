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

namespace EmailSimpleClientProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ProgramShutDown = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShutDown(object sender, EventArgs e)
        {
            ProgramShutDown = true;
        }

        private void SendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SmtpClient NewSmtpClient = new SmtpClient())
                {
                    MailAddress NewMailAddress = 
                }
                 
            }
            catch
            {

            }
            finally
            {

            }
        }

    }
}

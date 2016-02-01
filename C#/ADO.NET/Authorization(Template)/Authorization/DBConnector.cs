using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Authorization
{
    class DBConnector
    {
        public static SqlConnection ConnectionToDB;

        static DBConnector()
        {
            try
            {
                ConnectionToDB = new SqlConnection();
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionToDB.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionToAuthorizationDB"].
                    ConnectionString;
            }
            catch
            {
                MessageBox.Show(@"Не удалось создать подключение к базе данных!", @"Ошибка!",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public static void CheckLoginPassword(string CurrentLogin, string CurrentPassword)
        {
            try
            {
                bool access_denied_1 = true;
                bool access_denied_2 = true;

                ConnectionToDB.Open();

                string CommandString = @"SELECT Name,Password,InfoId FROM Users";
                SqlCommand command_test = new SqlCommand(CommandString, ConnectionToDB);

                SqlDataReader reader = command_test.ExecuteReader();

                while (reader.Read() != false)
                {
                    string NameTemp = reader.GetString(0).Replace(" ", "");
                    string PasswordTemp = reader.GetString(1).Replace(" ", "");
                    if (CurrentLogin == NameTemp)
                    {
                        access_denied_1 = false;
                    }
                    if (CurrentPassword == PasswordTemp)
                    {
                        access_denied_2 = false;
                    }
                    if (!access_denied_1 && !access_denied_2)
                    {
                        string test_string = reader[2].ToString();
                        MessageBox.Show(Properties.Resources.Welcome + " " + NameTemp + "!");
                        command_test.CommandText = @"SELECT FirstName,LastName,Adres,Phone,Code FROM UsersInfo WHERE Id="
                             + reader[2].ToString();
                        reader.Close();
                        SqlDataReader reader_2 = command_test.ExecuteReader();
                        reader_2.Read();

                        string FirstNameBuf = @"Фамилия:" + reader_2.GetString(0).Replace(" ", "");
                        string LastNameBuf = @"Имя:" + reader_2.GetString(1).Replace(" ", "");
                        string AdressBuf = @"Адрес:";
                        if (!reader_2.IsDBNull(2))
                        {
                            AdressBuf = AdressBuf + reader_2.GetString(2).Replace(" ", "");
                        }
                        else
                        {
                            AdressBuf = AdressBuf + @" -";
                        }
                        string PhoneBuf = @"Телефон:";
                        if (!reader_2.IsDBNull(3))
                        {
                            PhoneBuf = PhoneBuf + reader_2.GetString(3).Replace(" ", "");
                        }
                        else
                        {
                            PhoneBuf = PhoneBuf + @" -";
                        }
                        string CodeBuf = @"Код:" + reader_2.GetInt32(4).ToString();
                        MainForm NewMainForm = new MainForm(LastNameBuf + "\r\n" + FirstNameBuf + "\r\n" +
                            AdressBuf + "\r\n" + PhoneBuf + "\r\n" + CodeBuf);
                        NewMainForm.ShowDialog();
                        break;
                    }
                    else
                    {
                        access_denied_1 = true;
                        access_denied_2 = true;
                    }
                }
                if (access_denied_1 && access_denied_2)
                {
                    MessageBox.Show(@"Доступ запрещен!", @"Результат.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show(@"Не удалось получить доступ к базе данных! Наиболее вероятная причина - неверная строка подключения или отсутствие таковой в конфигурационном файле приложения.", 
                    @"Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConnectionToDB.Close();
            }
        }

        public static bool CreateNewPassword(string NewPassword, string CurrentEmail)
        {
            try
            {
                ConnectionToDB.Open();
                string CommandString = @"SELECT Email FROM Users";
                SqlCommand CommandExtractEmail = new SqlCommand(CommandString, ConnectionToDB);
                SqlDataReader EmailReader = CommandExtractEmail.ExecuteReader();
                while (EmailReader.Read() != false)
                {
                    string EmailTemp = EmailReader.GetString(0).Replace(" ", "");
                    if (CurrentEmail == EmailTemp)
                    {
                        SqlCommand CommandUpdate = new SqlCommand(@"UPDATE Users SET [Password]=@pas WHERE Email=@email",
                        ConnectionToDB);
                        CommandUpdate.Parameters.AddWithValue("@pas", NewPassword);
                        EmailReader.Close();
                        CommandUpdate.Parameters.AddWithValue("@email", EmailTemp);
                        CommandUpdate.ExecuteNonQuery();
                        MessageBox.Show("Your password cnanged!");
                        return true;
                    }
                }
            }
            catch
            {
                MessageBox.Show(@"Не удалось получить доступ к базе данных! Наиболее вероятная причина - неверная строка подключения или отсутствие таковой в конфигурационном файле приложения.",
                    @"Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConnectionToDB.Close();
            }
            return false;
        }
    }
}

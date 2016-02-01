using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;

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
            bool result = false;
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
                        result = true;
                        break;
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
            return result;
        }

        public static void CreateNewUser(string NewLogin, string NewPassword, string NewEmail, 
            string NewFirstName, string NewSecondName)
        {

                try
                {
                    ConnectionToDB.Open();

                    bool AccessDenied = false;
                    SqlCommand NewCommand = new SqlCommand();
                    NewCommand.Connection = ConnectionToDB;
                    NewCommand.CommandType = CommandType.StoredProcedure;
                    NewCommand.CommandText = "CheckLogin";
                    SqlParameter ReturnValue = new SqlParameter(@"ReturnValue", SqlDbType.Int);
                    ReturnValue.Direction = ParameterDirection.Output;
                    SqlParameter InputParam = new SqlParameter(@"CurrentLogin", SqlDbType.NChar,20);
                    InputParam.Direction = ParameterDirection.Input;
                    InputParam.Value = NewLogin;
                    NewCommand.Parameters.Add(InputParam);
                    NewCommand.Parameters.Add(ReturnValue);
                    NewCommand.ExecuteNonQuery();
                    if (Int32.Parse(NewCommand.Parameters[1].Value.ToString()) == 0)
                    {
                        AccessDenied = true;
                        MessageBox.Show("Your login already exists!", @"Error.", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                    if (!AccessDenied)
                    {
                        SqlTransaction CreateUserTransaction = ConnectionToDB.BeginTransaction();
                        try
                        {
                            SqlCommand NewCommand_2 = new SqlCommand();
                            NewCommand_2.Connection = ConnectionToDB;
                            NewCommand.Transaction = CreateUserTransaction;
                            NewCommand_2.CommandText = @"INSERT INTO UsersInfo (LastName, FirstName, Code) VALUES (@NewLastName, @NewFirstName, @ZeroValue)";

                            if (NewFirstName.Length == 0)
                                NewCommand_2.Parameters.AddWithValue("@NewLastName", "-");
                            else
                                NewCommand_2.Parameters.AddWithValue("@NewLastName", NewFirstName);
                            if (NewSecondName.Length == 0)
                                NewCommand_2.Parameters.AddWithValue("@NewFirstName", "-");
                            else
                                NewCommand_2.Parameters.AddWithValue("@NewFirstName", NewSecondName);
                            NewCommand_2.Parameters.AddWithValue("@ZeroValue", 0);
                            NewCommand_2.ExecuteNonQuery();
                            NewCommand_2.CommandText = @"SELECT MAX(Id) FROM UsersInfo";
                            int InfoIdBuf = Int32.Parse(NewCommand.ExecuteScalar().ToString());
                            NewCommand_2.CommandText = @"INSERT INTO Users VALUES (@NewName, @NewPassword, @NewEmail, @NewInfoId)";
                            NewCommand_2.Parameters.Clear();
                            NewCommand_2.Parameters.AddWithValue("@NewName", NewLogin);
                            NewCommand_2.Parameters.AddWithValue("@NewPassword", NewPassword);
                            NewCommand_2.Parameters.AddWithValue("@NewEmail", NewEmail);
                            NewCommand_2.Parameters.AddWithValue("@NewInfoId", InfoIdBuf);
                            NewCommand_2.ExecuteNonQuery();
                            CreateUserTransaction.Commit();
                            MessageBox.Show("New user successfully added to database.");
                        }
                        catch
                        {
                            try
                            {
                                CreateUserTransaction.Rollback();
                            }
                            catch (Exception RollbackException)
                            {
                                MessageBox.Show(RollbackException.Message,
                                    @"Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show(@"Не удалось создать нового пользователя!",
                                @"Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    }
                catch (Exception exep)
                {
                    MessageBox.Show(exep.Message);
                    MessageBox.Show(@"Не удалось получить доступ к базе данных! Наиболее вероятная причина - неверная строка подключения или отсутствие таковой в конфигурационном файле приложения.",
                        @"Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ConnectionToDB.Close();
                }       
        }
    }
}

/*CREATE PROCEDURE [dbo].[CheckLogin]
	@CurrentLogin NCHAR(20), @ReturnValue int OUTPUT
AS
	IF (EXISTS (SELECT Name FROM Users WHERE Name = @CurrentLogin))
		SET @ReturnValue = 0
	ELSE
		SET @ReturnValue = 1*/

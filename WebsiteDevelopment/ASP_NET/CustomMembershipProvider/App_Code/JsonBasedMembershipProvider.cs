using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using System.IO;

/// <summary>
/// Summary description for JsonBasedMembershipProvider
/// </summary>
public class JsonBasedMembershipProvider : MembershipProvider
{
    public class CustomUser
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string userPassword;

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }


        public CustomUser(string NewUserName, string NewUserPassword)
        {

        }
    }

    public static class DataStorageForJson
    {
        static List<CustomUser> currentCollectionOfUsers;
        static private object GatesToUsers = new object();

        public static List<CustomUser> CurrentCollectionOfUsers
        {
            get
            {
                return currentCollectionOfUsers;
            }
            set
            {
                currentCollectionOfUsers = value;
            }
        }
        public static void AddNewUser(CustomUser NewCustomUser)
        {
            lock (GatesToUsers)
            {
                CurrentCollectionOfUsers.Add(NewCustomUser);
            }
        }
    }

	public JsonBasedMembershipProvider()
	{

	}

    private string applicationName;

    public override string ApplicationName
    {
        get
        {
            return applicationName;
        }
        set
        {
            applicationName = value;
        }
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        CustomUser NewUser = new CustomUser(username, password);
        DataStorageForJson.AddNewUser(NewUser);
        status = MembershipCreateStatus.Success;
        return this.GetUser(username, true);
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
        get { throw new NotImplementedException(); }
    }

    public override bool EnablePasswordRetrieval
    {
        get { throw new NotImplementedException(); }
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        CustomUser FoundCustomUser = DataStorageForJson.CurrentCollectionOfUsers.Where(CurrentUser => CurrentUser.UserName == username).FirstOrDefault();
        if (FoundCustomUser != null)
        {
             MembershipUser FoundMembershipUser = new MembershipUser(  
                      this.Name,  
                      FoundCustomUser.UserName,  
                      null,  
                      "",  
                      "",  
                      "",  
                      true,  
                      false,  
                      DateTime.Now,  
                      DateTime.Now,  
                      DateTime.Now,  
                      default(DateTime),  
                      default(DateTime) 
                  );          
            return FoundMembershipUser;
        }
        else
        {
            return null;
        }
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
        get { throw new NotImplementedException(); }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
        get { throw new NotImplementedException(); }
    }

    public override int MinRequiredPasswordLength
    {
        get { throw new NotImplementedException(); }
    }

    public override int PasswordAttemptWindow
    {
        get { throw new NotImplementedException(); }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
        get { throw new NotImplementedException(); }
    }

    public override string PasswordStrengthRegularExpression
    {
        get { throw new NotImplementedException(); }
    }

    public override bool RequiresQuestionAndAnswer
    {
        get { return false; }
    }

    public override bool RequiresUniqueEmail
    {
        get { throw new NotImplementedException(); }
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }
    
    public override bool ValidateUser(string username, string password)
    {
        if (DataStorageForJson.CurrentCollectionOfUsers.Where(CurrentUser => CurrentUser.UserName == username && CurrentUser.UserPassword == password).FirstOrDefault() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
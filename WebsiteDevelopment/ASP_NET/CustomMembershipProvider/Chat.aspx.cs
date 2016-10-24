using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class DefaultPageForUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MembershipUser TestMembershipUser = Membership.GetUser();
        }
    }

    protected void GetLastChatMessages(object sender, EventArgs e)
    {
        RefreshDataSourceInRepeaterForChat();
    }

    protected void RefreshDataSourceInRepeaterForChat()
    {
        AccessorToApplication CurrentAccessorToApplication = new AccessorToApplication();
        RepeaterForChatMessages.DataSource = CurrentAccessorToApplication.GetLastChatMessages();
        RepeaterForChatMessages.DataBind();
    }

    protected void AddNewMessageToChatButtonClick(object sender, EventArgs e)
    {
        AccessorToApplication CurrentAccessorToApplication = new AccessorToApplication();
        UserMessage CurrentUserMessage = new UserMessage(TextBoxForTypingNewMessage.Text, Membership.GetUser().UserName);
        CurrentAccessorToApplication.AddNewMessageToChat(CurrentUserMessage);
        RefreshDataSourceInRepeaterForChat();
    }
}
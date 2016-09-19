using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    AllGuestsInfo CurrentGuestInfo = new AllGuestsInfo();
    StringBuilder TextForGuestsInfo = new StringBuilder("");
    protected void Page_Load(object sender, EventArgs e)
    {
   
    }

    protected void ShowListOfGuests(object sender, EventArgs e)
    {
        TextForGuestsInfo.Clear();
        foreach (string CurrentGuestName in CurrentGuestInfo.GetAllGuests())
        {
            TextForGuestsInfo.AppendLine(CurrentGuestName);
        }
        ListOfGuests.Text = "All guests: " + TextForGuestsInfo.ToString();
    }
    protected void AddNewGuest(object sender, EventArgs e)
    {
        CurrentGuestInfo.AddGuest(TextInput.Text);
        /*TextForAspLabel.Append(TextInput.Text);
        HelloWorldLabel.Text = TextForAspLabel.ToString();*/
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.Text;

public partial class AddPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonSendImagesOnServer_OnClick(object sender, EventArgs e)
    {
        if (FileUploadControl_ForImages.HasFiles)
        {
            List<string> ListOfFileNamesThatAlreadyExist = new List<string>();
            string PostedFileName = string.Empty;
            string ServerFullFileName = string.Empty;
            foreach (HttpPostedFile CurrentPostedFile in FileUploadControl_ForImages.PostedFiles)
            {
                PostedFileName = Path.GetFileName(CurrentPostedFile.FileName);
                ServerFullFileName = Texts.FullPathOfDirectoryForImages + "\\" + PostedFileName;
                if (!File.Exists(ServerFullFileName))
                {
                    CurrentPostedFile.SaveAs(ServerFullFileName);
                }
                else
                {
                    ListOfFileNamesThatAlreadyExist.Add(PostedFileName);
                }
            }
            if (ListOfFileNamesThatAlreadyExist.Count > 0)
            {
                StringBuilder StringBuilderForAlertMessage = new StringBuilder("Files below cannot be uploaded to the server, because files with such names already exist! :(");
                StringBuilderForAlertMessage.Append("\\n");
                foreach (string CurrentFileName in ListOfFileNamesThatAlreadyExist)
                {
                    StringBuilderForAlertMessage.Append("\\n" + CurrentFileName + ";");
                }
                ShowAlertMessage(StringBuilderForAlertMessage.ToString());
            }
            else
            {
                ShowAlertMessage("All files have been successfully uploaded to the server! :)");
            }
        }
    }

    protected void ButtonViewAllImagesOnServer_OnClick(object sender, EventArgs e)
    {
        this.Response.Redirect("ViewPhotos.aspx");
    }

    protected void ShowAlertMessage (string TextOfMessage)
    {
        StringBuilder TextOfScript = new StringBuilder();
        TextOfScript.Append("window.onload = function() {alert('" + TextOfMessage + "');};");
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "AlertMessageForResultOfUploadingFilesOnServer", TextOfScript.ToString(), true);
    }
}
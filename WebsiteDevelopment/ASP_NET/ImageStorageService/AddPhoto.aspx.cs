using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class AddPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonSendImagesOnServer_OnClick(object sender, EventArgs e)
    {
        if (FileUploadControl_ForImages.HasFiles)
        {
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
            }        
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Resources;

public partial class ViewPhotos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<ImageInStorage> AllImagesOnServer = new List<ImageInStorage>();
            foreach (string CurrentFileName in Directory.GetFiles(Texts.FullPathOfDirectoryForImages))
            {
                AllImagesOnServer.Add(new ImageInStorage(Path.GetFileNameWithoutExtension(CurrentFileName), Texts.DirectoryForImagesName + "/" + Path.GetFileName(CurrentFileName)));
            }

            RepeaterForShowingAllImages.DataSource = AllImagesOnServer;
            RepeaterForShowingAllImages.DataBind();
        }
    }

    protected void ButtonGoToPageForUploadingFiles_OnClick(object sender, EventArgs e)
    {
        this.Response.Redirect(Texts.PageNameForAddingPhotos);
    }
}
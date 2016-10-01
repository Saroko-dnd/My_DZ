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
            RepeaterForShowingAllImages.DataSource = ImagesInMemoryForRepeater.AllImagesOnServer;
            RepeaterForShowingAllImages.DataBind();
        }
    }
}
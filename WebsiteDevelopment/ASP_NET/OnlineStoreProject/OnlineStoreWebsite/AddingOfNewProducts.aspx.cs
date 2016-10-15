using Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddingOfNewProducts : System.Web.UI.Page
{
    private static object GatesForFileSaving = new object();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AddNewProductButtonOnClick(object sender, EventArgs e)
    {
        Validate();
        if (IsValid)
        {
            if (ProductImageFileUpload.HasFile)
            {
                string ServerFullFileName = string.Empty;
                ServerFullFileName = HttpContext.Current.Server.MapPath("/" + Texts.NameOfFolderForProductImages + "/" + ProductImageFileUpload.PostedFile.FileName);
                lock (GatesForFileSaving)
                {
                    if (!File.Exists(ServerFullFileName))
                    {
                        ProductImageFileUpload.PostedFile.SaveAs(ServerFullFileName);
                    }
                }
            }
            AccessorToSessionForListOfProductsPage CurrentAccessorToSession = new AccessorToSessionForListOfProductsPage(Session);
            CurrentAccessorToSession.GetManagerOfProductsForCurrentSession().AddNewProduct(ProductNameTextBox.Text, ProductDescriptionTextBox.Text, Int32.Parse(ProductPriceTextBox.Text),
                "~/" + Texts.NameOfFolderForProductImages + "/" + ProductImageFileUpload.PostedFile.FileName);
            CurrentListOfProducts.RefreshBinding();
        }
    }
}
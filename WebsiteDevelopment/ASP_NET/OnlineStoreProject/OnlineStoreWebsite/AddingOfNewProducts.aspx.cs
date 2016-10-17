using OnlineStoreObjects;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddingOfNewProducts : System.Web.UI.Page
{
    private static object GatesForFileSaving = new object();

    protected String CurrentColorForResultOfAddingNewProduct
    {
        get
        {
            return HiddenFieldForColorOfLabelWithResultOfAddingNewProject.Value;
        }
        set
        {
            HiddenFieldForColorOfLabelWithResultOfAddingNewProject.Value = value;
        }
    }

    protected String CurrentResultOfAddingNewProduct
    {
        get
        {
            return HiddenFieldForResultOfAddingNewProject.Value;
        }
        set
        {
            HiddenFieldForResultOfAddingNewProject.Value = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Just wcf service test *******
        /*NamespaceForWcfDataAccessToProducts.WCFRepositoryClient TestObjectForWCF = new NamespaceForWcfDataAccessToProducts.WCFRepositoryClient();
        IEnumerable TestCollectionOfProducts = TestObjectForWCF.GetAllData();*/ 
        //*****************************
        if (!IsPostBack)
        {
            LabelForResultOfAddingNewProject.CssClass = "MarginCenter HiddenControl";
            ConfirmResultButton.Attributes.Add("class", "MarginCenter HiddenControl ConspicuousText");
        }
        else
        {
            LabelForResultOfAddingNewProject.CssClass = "MarginCenter";
            LabelForResultOfAddingNewProject.Text = CurrentResultOfAddingNewProduct;
            LabelForResultOfAddingNewProject.ForeColor = Color.FromName(CurrentColorForResultOfAddingNewProduct);
            ConfirmResultButton.Attributes.Add("class", "MarginCenter ConspicuousText");
        }
    }

    protected void HandlerForAsyncPostBackErrors(object sender, AsyncPostBackErrorEventArgs e)
    {
        CurrentScriptManager.AsyncPostBackErrorMessage = e.Exception.Message;
    }

    protected void AddNewProductButtonOnClick(object sender, EventArgs e)
    {
        if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
        {
            if ((ProductNameTextBox.Text != null) && (ProductNameTextBox.Text != string.Empty))
            {
                AccessorToSessionForListOfProductsPage CurrentAccessorToSession = new AccessorToSessionForListOfProductsPage(Session);
                if (CurrentAccessorToSession.GetManagerOfProductsForCurrentSession().CheckProductNameForDuplicate(ProductNameTextBox.Text))
                {
                    throw new Exception(Texts.ErrorMessageForAddingNewProduct_IllegalName);
                }
            }
        }
        else
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

                bool NewProductWasAdded = CurrentAccessorToSession.GetManagerOfProductsForCurrentSession().AddNewProduct(ProductNameTextBox.Text, ProductDescriptionTextBox.Text,
                    Int32.Parse(ProductPriceTextBox.Text), "~/" + Texts.NameOfFolderForProductImages + "/" + ProductImageFileUpload.PostedFile.FileName);
                CurrentListOfProducts.RefreshBinding();
            }
        }
    }
}
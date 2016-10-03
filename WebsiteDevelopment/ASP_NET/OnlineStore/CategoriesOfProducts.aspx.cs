using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class CategoriesOfProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repeater_CategoriesOfProducts.DataSource = Application[Texts.KeyForListOfProductCategories];
            Repeater_CategoriesOfProducts.DataBind();
            Label_NameOfTheOnlineStore.Text = Application[Texts.KeyForNameOfTheOnlineStore] as string;
        }
    }

    protected void ButtonForSelectionOfProductCategory_OnClick(object sender, EventArgs e)
    {

    } 
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CategoriesOfProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repeater_CategoriesOfProducts.DataSource = Application["ListOfProductCategories"];
            Repeater_CategoriesOfProducts.DataBind();
            Label_NameOfTheOnlineStore.Text = Application["OnlineStoreName"] as string;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ListOfProductsForSelectedCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FakeRepository TestFakeRepository = new FakeRepository();
            DataGridForListOfProducts.DataSource = TestFakeRepository.GetAllData();
            DataGridForListOfProducts.DataBind();
            Session["ProductsForSelectedCategory"] = TestFakeRepository.GetAllData();
        }
    }

    protected void DataGridForListOfProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataGridForListOfProducts.PageIndex = e.NewPageIndex;
        DataGridForListOfProducts.DataSource = Session["ProductsForSelectedCategory"];
        DataGridForListOfProducts.DataBind();
    }
}
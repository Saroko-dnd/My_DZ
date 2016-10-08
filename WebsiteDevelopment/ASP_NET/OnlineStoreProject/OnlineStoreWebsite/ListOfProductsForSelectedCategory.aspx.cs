using OnlineStoreDataAccess;
using OnlineStoreObjects;
using OnlineStoreLogic;
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
            ManagerOfProducts CurrentManagerOfProducts = new ManagerOfProducts(new FakeRepositoryForProducts());
            Session["CurrentManagerOfProducts"] = CurrentManagerOfProducts; //Must be added to Session before DataSource because of User Control for likes and dislikes
            DataGridForListOfProducts.DataSource = CurrentManagerOfProducts.LoadProductsForPages();
            DataGridForListOfProducts.DataBind();
        }
    }

    protected void DataGridForListOfProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataGridForListOfProducts.PageIndex = e.NewPageIndex;
        DataGridForListOfProducts.DataSource = (Session["CurrentManagerOfProducts"] as ManagerOfProducts).LoadProductsForPages();
        DataGridForListOfProducts.DataBind();
    }
}
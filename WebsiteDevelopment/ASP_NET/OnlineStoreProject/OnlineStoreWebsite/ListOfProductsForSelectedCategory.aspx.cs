using OnlineStoreDataAccess;
using OnlineStoreObjects;
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
            FakeRepositoryForProducts TestRepositoryForProducts = new FakeRepositoryForProducts();
            DataGridForListOfProducts.DataSource = TestRepositoryForProducts.GetAllData();
            DataGridForListOfProducts.DataBind();
            Session["ProductsForSelectedCategory"] = TestRepositoryForProducts;
        }
    }

    protected void DataGridForListOfProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataGridForListOfProducts.PageIndex = e.NewPageIndex;
        DataGridForListOfProducts.DataSource = (Session["ProductsForSelectedCategory"] as FakeRepositoryForProducts).GetAllData();
        DataGridForListOfProducts.DataBind();
    }

    protected void DataGridForListOfProducts_SortingByPrice(object sender, GridViewSortEventArgs e)
    {      
        DataGridForListOfProducts.DataSource = (Session["ProductsForSelectedCategory"] as FakeRepositoryForProducts).GetAllDataSortedByPropertyReverse(e.SortExpression);
        DataGridForListOfProducts.DataBind();
    }
}
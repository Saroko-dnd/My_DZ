using OnlineStoreDataAccess;
using OnlineStoreLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GridViewForListOfProducts : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AccessorToSessionForListOfProductsPage CurrentAccessorToSession = new AccessorToSessionForListOfProductsPage(Session);
            CurrentAccessorToSession.AddManagerOfProductsToSession(new ManagerOfProducts(new FakeRepositoryForProducts()));
            DataGridForListOfProducts.DataSource = CurrentAccessorToSession.GetManagerOfProductsForCurrentSession().LoadProductsForPages();
            DataGridForListOfProducts.DataBind();
        }
    }

    protected void DataGridForListOfProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataGridForListOfProducts.PageIndex = e.NewPageIndex;
        AccessorToSessionForListOfProductsPage CurrentAccessorToSession = new AccessorToSessionForListOfProductsPage(Session);
        DataGridForListOfProducts.DataSource = CurrentAccessorToSession.GetManagerOfProductsForCurrentSession().LoadProductsForPages();
        DataGridForListOfProducts.DataBind();
    }
}
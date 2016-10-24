using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefreshCurrencyDataInGridView();
        }
    }

    protected void RefreshCurrencyDataOnTimerThick(object sender, EventArgs e)
    {
        RefreshCurrencyDataInGridView();
    }

    protected void RefreshCurrencyDataInGridView()
    {
        AccessorToCurrencyInfoService CurrentAccessorToCurrencyInfoService = new AccessorToCurrencyInfoService();
        GridViewForCurrency.DataSource = CurrentAccessorToCurrencyInfoService.GetCurrency();
        GridViewForCurrency.DataBind();
    }
}
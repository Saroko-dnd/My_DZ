using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using Resources;

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

    protected void HandlerForAsyncPostBackErrors(object sender, AsyncPostBackErrorEventArgs e)
    {
        CurrentScriptManager.AsyncPostBackErrorMessage = e.Exception.Message;
    }

    protected void RefreshCurrencyDataInGridView()
    {
        AccessorToCurrencyInfoService CurrentAccessorToCurrencyInfoService = new AccessorToCurrencyInfoService();
        IEnumerable<object> NewListOfCurrencyInfo = CurrentAccessorToCurrencyInfoService.GetCurrency();
        if (NewListOfCurrencyInfo != null)
        {
            GridViewForCurrency.DataSource = NewListOfCurrencyInfo;
            GridViewForCurrency.DataBind();
        }
        else
        {
            if (IsPostBack)
            {
                throw new Exception(Texts.Exception_ProgramCantRefreshCurrencyInfo);
            }
            else
            {
                LabelToShowOnExceptionDuringLoadData.Text = Texts.Exception_ProgramCantLoadCurrencyInfo;
            }

        }
    }
}
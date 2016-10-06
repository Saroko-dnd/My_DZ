using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DivisionOnline : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/JSfolder/jquery-3.1.1.min.js",
            });
        }
    }

    protected void ButtonForDivision_OnClick(object sender, EventArgs e)
    {
        Validate();
        if (IsValid)
        {
            double Dividend = Double.Parse(TextBoxForFirstNumber.Text);
            double Divider = Double.Parse(TextBoxForSecondNumber.Text);
            LabelForResultOfDivision.Text = (Dividend / Divider).ToString();
        }
    }
}
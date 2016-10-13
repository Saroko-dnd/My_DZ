using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddingOfNewProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AddNewProductButtonOnClick(object sender, EventArgs e)
    {
        Validate();
        if (IsValid)
        {

        }
    }
}
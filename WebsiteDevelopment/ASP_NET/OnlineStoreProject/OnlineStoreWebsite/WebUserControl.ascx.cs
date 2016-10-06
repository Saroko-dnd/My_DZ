using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControl : System.Web.UI.UserControl
{
    private int amountOfDislikes;

    public int AmountOfDislikes
    {
        get { return amountOfDislikes; }
        set { amountOfDislikes = value; }
    }
    private int amountOfLikes;

    public int AmountOfLikes
    {
        get { return amountOfLikes; }
        set { amountOfLikes = value; }
    }
    private int productID;

    public int ProductID
    {
        get { return productID; }
        set 
        { 

            productID = value; 
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

        }
    }
}
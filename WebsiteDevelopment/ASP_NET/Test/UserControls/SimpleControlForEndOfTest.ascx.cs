using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SimpleControlForEndOfTest : System.Web.UI.UserControl
{
    public void SetScore(uint UserScore, uint MaxScore)
    {
        ScoreLabel.Text = UserScore.ToString() + "/" + MaxScore.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
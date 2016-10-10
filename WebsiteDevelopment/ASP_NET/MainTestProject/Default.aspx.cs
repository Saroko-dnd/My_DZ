using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            List<TestClassForDetailsViewControl> DataSourceForDetailsViewTestControl = new List<TestClassForDetailsViewControl>();
            TestMultiView.ActiveViewIndex = 0;
            DataSourceForDetailsViewTestControl.Add(new TestClassForDetailsViewControl("First name 1", "Second name 1"));
            DataSourceForDetailsViewTestControl.Add(new TestClassForDetailsViewControl("First name 2", "Second name 2"));
            DataSourceForDetailsViewTestControl.Add(new TestClassForDetailsViewControl("First name 3", "Second name 3"));
            DetailsViewTestControl.DataSource = DataSourceForDetailsViewTestControl;
            DetailsViewTestControl.DataBind();
        }
    }

    protected void MultiViewNextStepButton_OnClick(object sender, EventArgs e)
    {
        if (TestMultiView.ActiveViewIndex < 2)
        {
            TestMultiView.ActiveViewIndex += 1;
        }
    }
}
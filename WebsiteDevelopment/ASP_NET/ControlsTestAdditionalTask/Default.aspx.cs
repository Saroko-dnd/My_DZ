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
        if (!this.IsPostBack)
        {
            DateTime Sunday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            List<string> ListOfDaysOfWeek = new List<string>();
            for (int CounterOfDaysOfWeek = 1; CounterOfDaysOfWeek < 8; ++CounterOfDaysOfWeek)
            {
                ListOfDaysOfWeek.Add(Sunday.AddDays(CounterOfDaysOfWeek).DayOfWeek.ToString());
            }
            DaysOfWeekCheckBoxList.DataSource = ListOfDaysOfWeek;
            DaysOfWeekCheckBoxList.DataBind();
            DaysOfWeekCheckBoxList.Items[5].Selected = true;
            DaysOfWeekCheckBoxList.Items[6].Selected = true;
        }
    }
}
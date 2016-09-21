using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainWebForm : System.Web.UI.Page
{
    static StringBuilder StringBuilderForTestLabel = new StringBuilder("");
    static object GatesToStringBuilder = new object();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(sender as Page).IsPostBack)
        {
            string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
            string[] systemEnvironmentColors =new string[(typeof(System.Drawing.SystemColors)).GetProperties().Length];

            int index = 0;

            foreach (MemberInfo member in (
                typeof(System.Drawing.SystemColors)).GetProperties())
            {
                systemEnvironmentColors[index++] = member.Name;
            }

            List<string> finalColorList = new List<string>();

            foreach (string color in allColors)
            {
                if (Array.IndexOf(systemEnvironmentColors, color) < 0)
                {
                    finalColorList.Add(color);
                }
            }
            DropDownListOfColors.DataSource = finalColorList;
            DropDownListOfColors.DataBind();
            MainTestLabel.Text = "Test of ASP Label control.";
        }
    }

    protected void AddTextButton_Click(object sender, EventArgs e)
    {
        lock(GatesToStringBuilder)
        {
            StringBuilderForTestLabel.Append("<br/>");
            StringBuilderForTestLabel.Append(MainInputTextBox.Text);
        }
        MainTestLabel.Text = StringBuilderForTestLabel.ToString();
    }

    protected void DropDownListOfColors_SelectedIndexChanged(object sender, EventArgs e)
    {
        MainTestLabel.ForeColor = Color.FromName((sender as DropDownList).SelectedItem.Text);
    }
}
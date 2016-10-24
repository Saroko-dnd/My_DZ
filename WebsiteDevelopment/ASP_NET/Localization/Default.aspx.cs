using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack && !string.IsNullOrEmpty(this.Label1.Text)) SetLabelText();
        this.Menu1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SetLabelText();
    }

    private void SetLabelText()
    {
        ResourceManager rm = new ResourceManager("Resources.CommonResource", Assembly.Load("App_GlobalResources"));
        this.Label1.Text = rm.GetString("HelloString");

    }

    protected override void InitializeCulture()
    {
        string language = Request.Form["__EventTarget"];
        string languageId = "";
        if (!string.IsNullOrEmpty(language))
        {
            if (language.EndsWith("French")) languageId = "fr-FR";
            else languageId = "en-US";
            SetCulture(languageId);
        }

        if (Session["Language"] != null)
        {
            if (!Session["Language"].ToString().StartsWith(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)) SetCulture(Session["Language"].ToString());
        }

        base.InitializeCulture();
    }

    protected void SetCulture(string languageId)
    {
        Session["Language"] = languageId;
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageId);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageId);
    }
}

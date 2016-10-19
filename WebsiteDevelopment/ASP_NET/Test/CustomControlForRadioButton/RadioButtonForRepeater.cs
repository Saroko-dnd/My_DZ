using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomControlForRadioButton
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RadioButtonForRepeater runat=server></{0}:RadioButtonForRepeater>")]
    public class RadioButtonForRepeater : RadioButton
    {
        public override bool Checked
        {
            get
            {
                if (Page.Request.Form[base.ID] == base.ClientID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                base.Checked = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {           
            writer.Write("<input id=\"" + base.ClientID + "\" ");
            writer.Write("type=\"radio\" ");
            writer.Write("name=\"" + base.ID + "\" ");
            writer.Write("value=\"" + base.ClientID + "\" />");
            writer.Write("<label for=\"" + base.ClientID + "\">");
            writer.Write(base.Text);
            writer.Write("</label>");
        }
    }
}

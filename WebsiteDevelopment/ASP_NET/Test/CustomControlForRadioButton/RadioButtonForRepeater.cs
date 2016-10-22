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
    public class RadioButtonForRepeater : RadioButton, IPostBackEventHandler
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

        public override string GroupName
        {
            get
            {
                return base.ID;
            }

            set
            {
                base.ID = value;
            }
        }

        private string Value
        {
            get
            {
                string val = base.ClientID;
                //val = val == null ? UniqueID : (UniqueID + "_") + val;
                return val;
            }
        }

        protected override bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            bool result = false;
            string value__1 = postCollection[base.ID];
            if ((value__1 != null) && (value__1 == Value))
            {
                if (!Checked)
                {
                    Checked = true;
                    result = true;
                }
            }
            else
            {
                if (Checked)
                {
                    RaisePostDataChangedEvent();
                }
            }
            return result;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            base.RaisePostDataChangedEvent();
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

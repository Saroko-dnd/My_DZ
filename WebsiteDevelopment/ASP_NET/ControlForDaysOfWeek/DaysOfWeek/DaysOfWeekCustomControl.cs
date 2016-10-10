using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DaysOfWeek
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class DaysOfWeekCustomControl : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null)? "[" + this.ID + "]" : s);
            }
 
            set
            {
                ViewState["Text"] = value;
            }
        }
 
        protected override void RenderContents(HtmlTextWriter output)
        {
            DateTime CurrentDate = DateTime.Today;
            DayOfWeek FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            while (CurrentDate.DayOfWeek != FirstDayOfWeek)
            {
                CurrentDate = CurrentDate.AddDays(-1);
            }

            DateTime EndDate = CurrentDate.AddDays(7);
            int NumberOfDays = (int)((EndDate - CurrentDate).TotalDays);
            List<DateTime> DatesForOneWeek = Enumerable
                //creates an IEnumerable of ints from 0 to numDays
                      .Range(0, NumberOfDays)
                //now for each of those numbers (0..numDays), 
                //select startDate plus x number of days
                      .Select(x => CurrentDate.AddDays(x))
                //and make a list
                      .ToList();
            DateTime TodayDate = DateTime.Now;

            foreach (DateTime CurrentDateTime in DatesForOneWeek)
            {
                if (CurrentDateTime.DayOfWeek == TodayDate.DayOfWeek)
                {
                    output.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
                    output.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
                    output.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "underline");
                }
                output.RenderBeginTag(HtmlTextWriterTag.P);
                output.Write(CurrentDateTime.DayOfWeek.ToString());
                output.RenderEndTag();
            }
        }
    }
}

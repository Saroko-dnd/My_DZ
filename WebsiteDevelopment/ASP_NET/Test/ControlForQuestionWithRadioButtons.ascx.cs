using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlForQuestionWithRadioButtons : System.Web.UI.UserControl
{
    private QuestionWithRadioButtons currentQuestionWithRadioButtons;

    public QuestionWithRadioButtons CurrentQuestionWithRadioButtons
    {
        get { return currentQuestionWithRadioButtons; }
        set { currentQuestionWithRadioButtons = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
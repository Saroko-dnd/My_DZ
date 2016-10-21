using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlForQuestionWithRadioButtons : System.Web.UI.UserControl, IControlForQuestion
{
    public void SetNewQuestion(QuestionWithOneCorrectAnswer NewQuestion)
    {
        LabelForTextOfQuestion.Text = NewQuestion.Text;
        HiddenFieldForQuestionID.Value = NewQuestion.ID.ToString();
        HiddenFieldForScore.Value = NewQuestion.Score.ToString();
        RepeaterForAnswers.DataSource = NewQuestion.CurrentCollectionOfAnswers;
        RepeaterForAnswers.DataBind();
    }

    public uint GetScore()
    {
        foreach (RepeaterItem CurrentItem in RepeaterForAnswers.Items)
        {
            RadioButton CurrentRadioButton = (RadioButton)CurrentItem.FindControl("RadioButtonForAnswer");
            HiddenField CurrentHiddenField = (HiddenField)CurrentItem.FindControl("HiddenField_IsAnswerCorrect");
            if (CurrentRadioButton.Checked && CurrentHiddenField.Value == true.ToString())
            {
                return UInt32.Parse(HiddenFieldForScore.Value);
            }
        }
        return 0;
    }

    public uint GetQuestionID()
    {
        return UInt32.Parse(HiddenFieldForQuestionID.Value);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
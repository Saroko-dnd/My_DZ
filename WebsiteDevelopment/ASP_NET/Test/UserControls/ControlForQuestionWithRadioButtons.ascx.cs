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
        AccessorToSession CurrentAccessorToSession = new AccessorToSession(Session);
        QuestionWithOneCorrectAnswer FoundQuestionWithSameID = (QuestionWithOneCorrectAnswer)CurrentAccessorToSession.CurrentTest.CurrentCollectonOfQuestions.
            Where(CurrentQuestion => CurrentQuestion.ID == UInt32.Parse(HiddenFieldForQuestionID.Value)).FirstOrDefault();
        int IndexOfAnswer = 0;
        RadioButton CurrentRadioButton = null;
        foreach (RepeaterItem CurrentItem in RepeaterForAnswers.Items)
        {
            CurrentRadioButton = (RadioButton)CurrentItem.FindControl("RadioButtonForAnswer");
            if (CurrentRadioButton.Checked)
            {
                if (FoundQuestionWithSameID.CurrentCollectionOfAnswers.ElementAt(IndexOfAnswer).CorrectAnswer)
                {
                    return UInt32.Parse(HiddenFieldForScore.Value);
                }
                else 
                {
                    return 0;
                }
            }
            ++IndexOfAnswer;
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
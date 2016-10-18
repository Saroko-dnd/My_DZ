using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;

public partial class ControlForTest : System.Web.UI.UserControl
{
    public void SetTestForThisControl(Test NewTest)
    {
        foreach (Question Currentquestion in NewTest.CurrentCollectonOfQuestions)
        {
            WizardStep NewWizardStep = new WizardStep();
            NewWizardStep.Controls.Add(CreateControlForQuestion(Currentquestion));
            CurrentWizardForTest.WizardSteps.Add(NewWizardStep);
        }
    }

    private UserControl CreateControlForQuestion(Question CurrentQuestion)
    {
        if (CurrentQuestion is QuestionWithOneCorrectAnswer)
        {
            ControlForQuestionWithRadioButtons CurrentControlForQuestion = new ControlForQuestionWithRadioButtons();
            CurrentControlForQuestion.SetNewQuestion(CurrentQuestion as QuestionWithOneCorrectAnswer);
            return CurrentControlForQuestion;
        }
        else
        {
            return null;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
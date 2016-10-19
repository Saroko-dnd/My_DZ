using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;

public partial class ControlForTest : System.Web.UI.UserControl
{
    public void SetTestForThisControl(Test NewTest, UserControl ControlForResultOfTest)
    {
        if (NewTest.CurrentCollectonOfQuestions.Count() == 0)
        {
            throw new ArgumentException("Object of type Test must have at least one question");
        }
        try
        {
            Label LabelFoScore = (Label)ControlForResultOfTest.FindControl("ScoreLabel");
        }
        catch
        {
            throw new ArgumentException("UserControl for result of test must have label with id='ScoreLabel'");
        }
        foreach (Question Currentquestion in NewTest.CurrentCollectonOfQuestions)
        {
            WizardStep NewWizardStep = new WizardStep();
            NewWizardStep.Controls.Add(CreateControlForQuestion(Currentquestion));
            CurrentWizardForTest.WizardSteps.Add(NewWizardStep);
        }
        WizardStep LastWizardStep = new WizardStep();
        LastWizardStep.Controls.Add(ControlForResultOfTest);
        CurrentWizardForTest.WizardSteps.Add(LastWizardStep);
        int IndexOfLastWizardStep = CurrentWizardForTest.WizardSteps.Count - 1;
        int IndexOfFinishStep = CurrentWizardForTest.WizardSteps.Count - 2;
        if (IndexOfFinishStep == 0)
        {
            CurrentWizardForTest.WizardSteps[IndexOfFinishStep].StepType = WizardStepType.Finish;
            CurrentWizardForTest.WizardSteps[IndexOfFinishStep].AllowReturn = false;
            CurrentWizardForTest.WizardSteps[IndexOfLastWizardStep].StepType = WizardStepType.Complete;
        }
        else
        {
            for (int Index = 0; Index < CurrentWizardForTest.WizardSteps.Count; ++Index)
            {
                if (Index == 0)
                {
                    CurrentWizardForTest.WizardSteps[Index].StepType = WizardStepType.Start;
                }
                else if (Index == IndexOfLastWizardStep)
                {
                    CurrentWizardForTest.WizardSteps[Index].StepType = WizardStepType.Complete;
                }
                else if (Index == IndexOfFinishStep)
                {
                    CurrentWizardForTest.WizardSteps[Index].StepType = WizardStepType.Finish;
                }
                else
                {
                    CurrentWizardForTest.WizardSteps[Index].StepType = WizardStepType.Step;
                }
            }
        }
    }

    protected void FinishButtonClickEventHandlerForWizard (object Sender, WizardNavigationEventArgs e)
    {
        uint BufferForUserScore = 0;
        for (int Index = 0; Index < CurrentWizardForTest.WizardSteps.Count - 1; ++Index)
        {
            BufferForUserScore += (CurrentWizardForTest.WizardSteps[Index].Controls[0] as IControlForQuestion).GetScore();
        }
        AccessorToSession CurrentAccessorToSession = new AccessorToSession(Session);
        SimpleControlForEndOfTest CurrentControlForEndOfTest = (SimpleControlForEndOfTest)CurrentWizardForTest.WizardSteps[CurrentWizardForTest.WizardSteps.Count - 1].Controls[0];
        CurrentControlForEndOfTest.SetScore(BufferForUserScore, CurrentAccessorToSession.CurrentTest.MaxScore);
    }


    private UserControl CreateControlForQuestion(Question CurrentQuestion)
    {
        if (CurrentQuestion is QuestionWithOneCorrectAnswer)
        {
            ControlForQuestionWithRadioButtons CurrentControlForQuestion = Page.LoadControl("~/UserControls/ControlForQuestionWithRadioButtons.ascx") as ControlForQuestionWithRadioButtons;
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
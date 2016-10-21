using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;
using Resources;

public partial class ControlForTest : System.Web.UI.UserControl
{
    public static class TestStates
    {
        public static readonly string InProgress = "InProgress";
        public static readonly string Completed = "Completed";
    }

    public void StopTest()
    {
        CountUserScore();
        CurrentWizardForTest.ActiveStepIndex = CurrentWizardForTest.WizardSteps.Count - 1;
    }

    private void CountUserScore()
    {
        uint BufferForUserScore = 0;
        AccessorToSession CurrentAccessorToSession = new AccessorToSession(Session);
        Dictionary<uint, uint> CurrentCollectionOfUserScore = CurrentAccessorToSession.CurrentUserScoreCollection;
        for (int Index = 0; Index < CurrentWizardForTest.WizardSteps.Count - 1; ++Index)
        {
            BufferForUserScore += CurrentCollectionOfUserScore[(CurrentWizardForTest.WizardSteps[Index].Controls[0] as IControlForQuestion).GetQuestionID()];
        }
        SimpleControlForEndOfTest CurrentControlForEndOfTest = (SimpleControlForEndOfTest)CurrentWizardForTest.WizardSteps[CurrentWizardForTest.WizardSteps.Count - 1].Controls[0];
        CurrentControlForEndOfTest.SetScore(BufferForUserScore, CurrentAccessorToSession.CurrentTest.MaxScore);
        HiddenFieldForStateOfTestWizard.Value = TestStates.Completed;
    }

    public void SetTestForThisControl(Test NewTest, UserControl ControlForResultOfTest, bool UserScoreCollectionNeedToBeAddedToSession)
    {
        if (NewTest.CurrentCollectonOfQuestions.Count() == 0)
        {
            throw new ArgumentException(Texts.ExceptionMessageForTestControl_TestIsEmpty);
        }
        try
        {
            Label LabelFoScore = (Label)ControlForResultOfTest.FindControl("ScoreLabel");
        }
        catch
        {
            throw new ArgumentException(Texts.ExceptionMessageForTestControl_LabelForScoreIsMissing);
        }
        HiddenFieldForStateOfTestWizard.Value = TestStates.InProgress;
        AccessorToSession CurrentAccessorToSession = new AccessorToSession(Session);
        foreach (Question Currentquestion in NewTest.CurrentCollectonOfQuestions)
        {
            WizardStep NewWizardStep = new WizardStep();
            NewWizardStep.Controls.Add(CreateControlForQuestion(Currentquestion));
            CurrentWizardForTest.WizardSteps.Add(NewWizardStep);
        }
        if (UserScoreCollectionNeedToBeAddedToSession)
        {
            Dictionary<uint, uint> CollectionOfUserScore = new Dictionary<uint, uint>();
            foreach (Question Currentquestion in NewTest.CurrentCollectonOfQuestions)
            {
                CollectionOfUserScore[Currentquestion.ID] = 0;
            }
            CurrentAccessorToSession.CurrentUserScoreCollection = CollectionOfUserScore;
        }
        WizardStep LastWizardStep = new WizardStep();
        LastWizardStep.Controls.Add(ControlForResultOfTest);
        CurrentWizardForTest.WizardSteps.Add(LastWizardStep);
        int IndexOfLastWizardStep = CurrentWizardForTest.WizardSteps.Count - 1;
        int IndexOfFinishStep = CurrentWizardForTest.WizardSteps.Count - 2;
        ConfigureWizardSteps(IndexOfLastWizardStep, IndexOfFinishStep);
    }

    private void ConfigureWizardSteps(int IndexOfLastWizardStep, int IndexOfFinishStep)
    {
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

    protected void NextButtonClickEventHandlerForWizard(object Sender, WizardNavigationEventArgs e)
    {
        UpdateUserScore();
    }

    protected void UpdateUserScore()
    {
        IControlForQuestion CurrentControlForQuestion = CurrentWizardForTest.ActiveStep.Controls[0] as IControlForQuestion;
        uint CurrentQuestionID = CurrentControlForQuestion.GetQuestionID();
        AccessorToSession CurrentAccessorToSession = new AccessorToSession(Session);
        Dictionary<uint, uint> TestDictionary = CurrentAccessorToSession.CurrentUserScoreCollection;
        CurrentAccessorToSession.CurrentUserScoreCollection[CurrentQuestionID] = CurrentControlForQuestion.GetScore();
    }

    protected void FinishButtonClickEventHandlerForWizard (object Sender, WizardNavigationEventArgs e)
    {
        UpdateUserScore();
        CountUserScore();
    }


    private UserControl CreateControlForQuestion(Question CurrentQuestion)
    {
        if (CurrentQuestion is QuestionWithOneCorrectAnswer)
        {
            ControlForQuestionWithRadioButtons CurrentControlForQuestion = Page.LoadControl(VirtualPathsToUserControls.ControlForQuestionWithRadioButtons) as ControlForQuestionWithRadioButtons;
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
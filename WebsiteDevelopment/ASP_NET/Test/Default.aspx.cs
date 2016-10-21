using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Init(object sender,EventArgs e)
    {
        if (IsPostBack)
        {
            ControlForTest NewControlForTest = Page.LoadControl(VirtualPathsToUserControls.ControlForTest) as ControlForTest;
            SimpleControlForEndOfTest CurrentTestControlForEndOfTest = Page.LoadControl(VirtualPathsToUserControls.SimpleControlForEndOfTest) as SimpleControlForEndOfTest;
            AccessorToSession CurrentAccessorToSession = new AccessorToSession(Session);
            NewControlForTest.SetTestForThisControl(CurrentAccessorToSession.CurrentTest, CurrentTestControlForEndOfTest, false);
            PanelForTestControl.Controls.Add(NewControlForTest);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlForTest NewControlForTest = Page.LoadControl(VirtualPathsToUserControls.ControlForTest) as ControlForTest;
            SimpleControlForEndOfTest CurrentTestControlForEndOfTest = Page.LoadControl(VirtualPathsToUserControls.SimpleControlForEndOfTest) as SimpleControlForEndOfTest;
            List<AnswerForQuestionWithOneCorrectAnswer> TestListOfAnswers = new List<AnswerForQuestionWithOneCorrectAnswer>();
            TestListOfAnswers.Add(new AnswerForQuestionWithOneCorrectAnswer("Answer 1", true));
            TestListOfAnswers.Add(new AnswerForQuestionWithOneCorrectAnswer("Answer 2", false));
            TestListOfAnswers.Add(new AnswerForQuestionWithOneCorrectAnswer("Answer 3", false));
            List<QuestionWithOneCorrectAnswer> TestListOfQuestions = new List<QuestionWithOneCorrectAnswer>();
            for (uint CounterOfQuestions = 0; CounterOfQuestions < 5; ++CounterOfQuestions)
            {
                TestListOfQuestions.Add(new QuestionWithOneCorrectAnswer(CounterOfQuestions, 5, "Question_" + CounterOfQuestions.ToString(), TestListOfAnswers));
            }
            Test CurrentTestObject = new Test(TestListOfQuestions);
            AccessorToSession CurrentAccessorToSession = new AccessorToSession(Session);
            CurrentAccessorToSession.CurrentTest = CurrentTestObject;
            NewControlForTest.SetTestForThisControl(CurrentTestObject, CurrentTestControlForEndOfTest, true);
            PanelForTestControl.Controls.Add(NewControlForTest);
        }
    }

    protected void TimeExpiredEventHandler(object sender, EventArgs e)
    {
        ControlForTest CurrentControlForTest = (ControlForTest)PanelForTestControl.Controls.Cast<Control>().Where(CurrentControl => CurrentControl is ControlForTest).FirstOrDefault();
        CurrentControlForTest.StopTest();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Question
/// </summary>
public class QuestionWithRadioButtons : Question
{
    private string currentQuestion;

    public string CurrentQuestion
    {
        get { return currentQuestion; }
        set { currentQuestion = value; }
    }

    private IEnumerable<string> currentCollectionOfAnswers;

    public IEnumerable<string> CurrentCollectionOfAnswers
    {
        get { return currentCollectionOfAnswers; }
        set { currentCollectionOfAnswers = value; }
    }
    public QuestionWithRadioButtons(string NewQuestion, IEnumerable<string> CollectionOfAnswers)
	{
        CurrentQuestion = NewQuestion;
        CurrentCollectionOfAnswers = CollectionOfAnswers;
	}
}
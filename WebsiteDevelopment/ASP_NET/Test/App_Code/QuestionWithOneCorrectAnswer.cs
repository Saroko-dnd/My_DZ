using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Question
/// </summary>
public class QuestionWithOneCorrectAnswer : Question
{
    private IEnumerable<AnswerForQuestionWithOneCorrectAnswer> currentCollectionOfAnswers;

    public IEnumerable<AnswerForQuestionWithOneCorrectAnswer> CurrentCollectionOfAnswers
    {
        get
        {
            return currentCollectionOfAnswers;
        }
        set
        {
            currentCollectionOfAnswers = value;
        }
    }

    public QuestionWithOneCorrectAnswer(uint QuestionID, uint ScoreForQuestion, string TextOfQuestion, IEnumerable<AnswerForQuestionWithOneCorrectAnswer> CollectionOfAnswers)
	{
        ID = QuestionID;
        Score = ScoreForQuestion;
        Text = TextOfQuestion;
        CurrentCollectionOfAnswers = CollectionOfAnswers;
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для Answer
/// </summary>
public class AnswerForQuestionWithOneCorrectAnswer
{
    private string text;
    private bool correctAnswer;

    public string Text
    {
        get
        {
            return text;
        }

        set
        {
            text = value;
        }
    }

    public bool CorrectAnswer
    {
        get
        {
            return correctAnswer;
        }

        set
        {
            correctAnswer = value;
        }
    }

    public AnswerForQuestionWithOneCorrectAnswer(string TextOfAnswer, bool ThisIsCorrectAnswer)
    {
        Text = TextOfAnswer;
        CorrectAnswer = ThisIsCorrectAnswer;
    }
}
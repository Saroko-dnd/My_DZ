using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Test
/// </summary>
public class Test
{
    IEnumerable<Question> currentCollectonOfQuestions;
    private uint maxScore = 0;

    public IEnumerable<Question> CurrentCollectonOfQuestions
    {
        get
        {
            return currentCollectonOfQuestions;
        }
        set
        {
            currentCollectonOfQuestions = value;
        }
    }

    public uint MaxScore
    {
        get
        {
            return maxScore;
        }

        private set
        {
            maxScore = value;
        }
    }

    public Test(IEnumerable<Question> CollectonOfQuestions)
	{
        CurrentCollectonOfQuestions = CollectonOfQuestions;
        foreach (Question CurrentQuestion in CollectonOfQuestions)
        {
            MaxScore += CurrentQuestion.Score;
        }
	}
}
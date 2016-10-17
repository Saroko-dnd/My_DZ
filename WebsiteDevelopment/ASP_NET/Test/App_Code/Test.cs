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

    public IEnumerable<Question> CurrentCollectonOfQuestions
    {
        get { return currentCollectonOfQuestions; }
        set { currentCollectonOfQuestions = value; }
    }
    public Test(IEnumerable<Question> CollectonOfQuestions)
	{
        CurrentCollectonOfQuestions = CollectonOfQuestions;
	}
}
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
    private uint hours;
    private uint minutes;
    private uint seconds;

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

    public uint Hours
    {
        get
        {
            return hours;
        }

        set
        {
            hours = value;
        }
    }

    public uint Minutes
    {
        get
        {
            return minutes;
        }

        set
        {
            minutes = value;
        }
    }

    public uint Seconds
    {
        get
        {
            return seconds;
        }

        set
        {
            seconds = value;
        }
    }

    public Test(IEnumerable<Question> CollectonOfQuestions, uint HoursForTest, uint MinutesForTest, uint SecondsForTest)
	{
        if (HoursForTest == 0 && MinutesForTest == 0 && SecondsForTest == 0)
        {
            Hours = 0;
            Minutes = 15;
            Seconds = 0; 
        }
        else
        {
            Hours = HoursForTest;
            Minutes = MinutesForTest;
            Seconds = SecondsForTest;
            if (Seconds > 59)
            {
                Minutes += Seconds / 60;
                Seconds = Seconds % 60;
            }
            if (Minutes > 59)
            {
                Hours += Minutes / 60;
                Minutes = Minutes % 60;
            }
        }
        CurrentCollectonOfQuestions = CollectonOfQuestions;
        foreach (Question CurrentQuestion in CollectonOfQuestions)
        {
            MaxScore += CurrentQuestion.Score;
        }
	}
}
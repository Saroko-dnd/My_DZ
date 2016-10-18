using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for IQuestion
/// </summary>
public abstract class Question
{
    private uint id;
    private uint score;
    private string text;

    public uint ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public uint Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

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
}
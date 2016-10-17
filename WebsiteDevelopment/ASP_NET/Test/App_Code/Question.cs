using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for IQuestion
/// </summary>
abstract class Question
{
    private uint id;

    public uint ID
    {
        get { return id;}
        set { id = value; }
    }
}
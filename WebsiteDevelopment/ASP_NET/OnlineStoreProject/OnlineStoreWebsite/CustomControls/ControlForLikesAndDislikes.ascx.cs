﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineStoreLogic;
using OnlineStoreObjects;

public partial class ControlForLikesAndDislikes : System.Web.UI.UserControl
{
    private int currentProductID_Int;

    public int CurrentProductID_Int
    {
        get
        {
            return Convert.ToInt32(ViewState["CurrentProductID_Int"]);
        }

        set
        {
            ViewState["CurrentProductID_Int"] = value;
            Product CurrentProduct = (Session["CurrentManagerOfProducts"] as ManagerOfProducts).GetProductByID(value);
            if (CurrentProduct != null)
            {
                LabelForLikes.Text = CurrentProduct.Likes.ToString();
                LabelForDislikes.Text = CurrentProduct.Dislikes.ToString();
            }
        }
    }

    protected void LikeButton_OnClick(object sender, EventArgs e)
    {
        int NewAmountOfLikes = (Session["CurrentManagerOfProducts"] as ManagerOfProducts).ChangeRating(CurrentProductID_Int, true);
        LabelForLikes.Text = NewAmountOfLikes.ToString();
    }

    protected void DislikeButton_OnClick(object sender, EventArgs e)
    {
        int NewAmountOfLikes = (Session["CurrentManagerOfProducts"] as ManagerOfProducts).ChangeRating(CurrentProductID_Int, false);
        LabelForDislikes.Text = NewAmountOfLikes.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
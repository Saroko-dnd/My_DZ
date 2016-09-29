using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!(sender as Page).IsPostBack)
        {
            ListBoxProducts.DataSource = new List<string>() { "Product_1", "Product_2", "Product_3", "Product_4", "Product_5" };
            ListBoxProducts.DataBind();
        }
    }

    protected void AddSelectedToBasket_ButtonClick(object sender, EventArgs e)
    {
        ListItemCollection ItemsForBasketInProductsListBox = new ListItemCollection();
        foreach (ListItem CurrentItem in this.ListBoxProducts.Items)
        {
            if (CurrentItem.Selected)
            {
                this.ListBoxSelectedProducts.Items.Add(CurrentItem);
                ItemsForBasketInProductsListBox.Add(CurrentItem);
            }
        }
        foreach (ListItem CurrentItem in ItemsForBasketInProductsListBox)
        {
            this.ListBoxProducts.Items.Remove(CurrentItem);
        }  
    }

    protected void RemoveSelectedFromBasket_ButtonClick(object sender, EventArgs e)
    {
        ListItemCollection ItemsForRemoveActionInBasketListBox = new ListItemCollection();
        foreach (ListItem CurrentItem in this.ListBoxSelectedProducts.Items)
        {
            if (CurrentItem.Selected)
            {
                this.ListBoxProducts.Items.Add(CurrentItem);
                ItemsForRemoveActionInBasketListBox.Add(CurrentItem);
            }
        }
        foreach (ListItem CurrentItem in ItemsForRemoveActionInBasketListBox)
        {
            this.ListBoxSelectedProducts.Items.Remove(CurrentItem);
        }  
    }

    protected void PutAllInBasket_ButtonClick(object sender, EventArgs e)
    {
        foreach (ListItem CurrentItem in this.ListBoxProducts.Items)
        {
            this.ListBoxSelectedProducts.Items.Add(CurrentItem);
        }
        this.ListBoxProducts.Items.Clear();
    }

    protected void RemoveAllFromBasket_ButtonClick(object sender, EventArgs e)
    {
        foreach (ListItem CurrentItem in this.ListBoxSelectedProducts.Items)
        {
            this.ListBoxProducts.Items.Add(CurrentItem);
        }
        this.ListBoxSelectedProducts.Items.Clear();
    }
}
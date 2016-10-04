using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для Product
/// </summary>
public class Product
{
    private string imageURL;
    private string name;
    private string description = string.Empty;
    private int price;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Description
    {
        get
        {
            return description;
        }

        private set
        {
            description = value;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    public string ImageURL
    {
        get
        {
            return imageURL;
        }

        set
        {
            imageURL = value;
        }
    }

    public Product(string NewName,string NewDescription, int NewPrice, string NewImageURL)
    {
        Name = NewName;
        Description = NewDescription;
        Price = NewPrice;
        ImageURL = NewImageURL;
    }
}
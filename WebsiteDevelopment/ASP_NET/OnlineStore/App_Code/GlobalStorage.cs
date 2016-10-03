using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Сводное описание для GlobalStorage
/// </summary>
public class ProductCategory
{
    private string productCategoryName = string.Empty;

    public string ProductCategoryName
    {
        get
        {
            return productCategoryName;
        }

        set
        {
            productCategoryName = value;
        }
    }

    public ProductCategory(string NewNameForProductCategory)
    {
        ProductCategoryName = NewNameForProductCategory;
    }
}
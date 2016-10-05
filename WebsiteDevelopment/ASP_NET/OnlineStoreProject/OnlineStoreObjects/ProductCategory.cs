using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreObjects
{
    public class ProductCategory
    {
        private List<Product> products;
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

        public List<Product> Products
        {
            get
            {
                return products;
            }

            set
            {
                products = value;
            }
        }

        public ProductCategory(string NewNameForProductCategory, List<Product> NewListOfProducts)
        {
            ProductCategoryName = NewNameForProductCategory;
            Products = NewListOfProducts;
        }
    }
}

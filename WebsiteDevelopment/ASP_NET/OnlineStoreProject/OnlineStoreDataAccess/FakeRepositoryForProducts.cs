using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStoreObjects;
using System.Reflection;

namespace OnlineStoreDataAccess
{
    public class FakeRepositoryForProducts : IRepository<Product>
    {
        protected List<Product> ListOfProducts = new List<Product>();

        public FakeRepositoryForProducts()
        {

        }

        public IEnumerable<Product> GetAllData()
        {
            if (ListOfProducts.Count == 0)
            {
                ListOfProducts.Add(new Product("Name 1", "Description 1", 100, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 2", "Description 2", 200, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 3", "Description 3", 300, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 4", "Description 4", 400, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 5", "Description 5", 500, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 6", "Description 6", 600, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 7", "Description 7", 700, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 8", "Description 8", 800, "ProductImages/Category_1/TestImage.jpg"));
                ListOfProducts.Add(new Product("Name 9", "Description 9", 900, "ProductImages/Category_1/TestImage.jpg"));
            }
            return ListOfProducts;
        }
    }
}

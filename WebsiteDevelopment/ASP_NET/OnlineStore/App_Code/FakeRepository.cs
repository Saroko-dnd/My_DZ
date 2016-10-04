using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FakeRepository
/// </summary>
public class FakeRepository : IRepository<Product>
{
    protected List<Product> ListOfProducts = new List<Product>();

	public FakeRepository()
	{
        ListOfProducts.Add(new Product("Name 1", "Description 1", 100, "ProductImages/Category_1/TestImage.jpg"));
        ListOfProducts.Add(new Product("Name 2", "Description 2", 200, "ProductImages/Category_1/TestImage.jpg"));
        ListOfProducts.Add(new Product("Name 3", "Description 3", 300, "ProductImages/Category_1/TestImage.jpg"));
        ListOfProducts.Add(new Product("Name 4", "Description 4", 400, "ProductImages/Category_1/TestImage.jpg"));
        ListOfProducts.Add(new Product("Name 5", "Description 5", 500, "ProductImages/Category_1/TestImage.jpg"));
        ListOfProducts.Add(new Product("Name 6", "Description 6", 600, "ProductImages/Category_1/TestImage.jpg"));
        ListOfProducts.Add(new Product("Name 7", "Description 7", 700, "ProductImages/Category_1/TestImage.jpg"));
        ListOfProducts.Add(new Product("Name 8", "Description 8", 800, "ProductImages/Category_1/TestImage.jpg"));
	}

    public IEnumerable<Product> GetAllData()
    {
        return ListOfProducts;
    }
}
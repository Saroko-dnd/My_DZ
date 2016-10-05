using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStoreObjects;
using OnlineStoreDataAccess;

namespace OnlineStoreLogic
{
    public class FakeRepositoryForProducts : IRepository<Product>
    {
        protected List<Product> ListOfProducts = new List<Product>();

        public FakeRepositoryForProducts()
        {
            FakeDataAccessToProducts CurrentDataAccessToProducts = new FakeDataAccessToProducts();
            ListOfProducts = (List<Product>)CurrentDataAccessToProducts.LoadData();
        }

        public IEnumerable<Product> GetAllData()
        {
            return ListOfProducts;
        }

        public IEnumerable<Product> GetAllDataSortedByProperty(string PropertyName)
        {
            return ListOfProducts.OrderBy(CurrentProduct => CurrentProduct.GetType().GetProperty(PropertyName).GetValue(CurrentProduct, null));
        }
    }
}

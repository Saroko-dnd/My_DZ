using OnlineStoreDataAccess;
using OnlineStoreObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceLibraryForProductsData
{
    public class WCFFakeRepositoryForProducts : IWCFRepository
    {
        protected FakeRepositoryForProducts CurrentFakeRepositoryForProducts = new FakeRepositoryForProducts();

        public WCFFakeRepositoryForProducts()
        {

        }

        public List<Product> GetAllData()
        {
            return CurrentFakeRepositoryForProducts.GetAllData().ToList();
        }
    }
}

using OnlineStoreDataAccess;
using OnlineStoreObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceLibraryForProductsData
{
    class WCFFakeRepositoryForProducts : IWCFRepository<Product>
    {
        protected FakeRepositoryForProducts CurrentFakeRepositoryForProducts = new FakeRepositoryForProducts();

        public WCFFakeRepositoryForProducts()
        {

        }

        public IEnumerable<Product> GetAllData()
        {
            return CurrentFakeRepositoryForProducts.GetAllData();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStoreObjects;
using System.Collections;

namespace OnlineStoreLogic
{
    public class ManagerOfProducts
    {
        private IRepository<Product> CurrentRepository;

        public int ChangeRating(int CurrentPropductID, bool Increase)
        {
            Product FoundProduct = CurrentRepository.GetAllData().Where(CurrentProduct => CurrentProduct.ProductID == CurrentPropductID).FirstOrDefault();
            if (FoundProduct != null)
            {
                if (Increase)
                {
                    FoundProduct.Likes += 1;
                    return FoundProduct.Likes;
                }
                else
                {
                    FoundProduct.Dislikes += 1;
                    return FoundProduct.Dislikes;
                }
            }
            return -1;
        }

        public void AddNewProduct(string NewProductName, string NewProductDescription, int NewPrice, string NewImageUrl)
        {
            (CurrentRepository.GetAllData() as List<Product>).Add(new Product(NewProductName, NewProductDescription, NewPrice, NewImageUrl));
        }

        public Product GetProductByID(int CurrentPropductID)
        {
            Product FoundProduct = CurrentRepository.GetAllData().Where(CurrentProduct => CurrentProduct.ProductID == CurrentPropductID).FirstOrDefault();
            return FoundProduct;
        }

        public IEnumerable<Product> LoadProductsForPages()
        {
            return CurrentRepository.GetAllData();
        }

        public ManagerOfProducts(IRepository<Product> NewCurrentRepository)
        {
            CurrentRepository = NewCurrentRepository;
        }
    }
}

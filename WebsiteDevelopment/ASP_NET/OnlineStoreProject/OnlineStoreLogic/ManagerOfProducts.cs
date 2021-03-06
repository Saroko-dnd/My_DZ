﻿using System;
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

        public bool CheckProductNameForDuplicate(string CurrentNameOfProduct)
        {
            if (CurrentRepository.GetAllData().Where(CurrentProduct => CurrentProduct.Name == CurrentNameOfProduct).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AddNewProduct(string NewProductName, string NewProductDescription, int NewPrice, string NewImageUrl)
        {
            if (CurrentRepository.GetAllData().Where(CurrentProduct => CurrentProduct.Name == NewProductName).FirstOrDefault() == null)
            {
                Product BufferForNewProduct = new Product(NewProductName, NewProductDescription, NewPrice, NewImageUrl);
                BufferForNewProduct.ProductID = CurrentRepository.GetAllData().Count() + 1;
                (CurrentRepository.GetAllData() as List<Product>).Add(BufferForNewProduct);
                return true;
            }
            else
            {
                return false;
            }

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

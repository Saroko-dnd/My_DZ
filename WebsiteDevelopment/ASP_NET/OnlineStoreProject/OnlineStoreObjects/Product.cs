using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreObjects
{
    public class Product
    {
        private int productID;
        private string imageURL;
        private string name;
        private string description = string.Empty;
        private int price;
        private int likes = 1;
        private int dislikes = 1;
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

        public int ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                productID = value;
            }
        }

        public int Likes
        {
            get
            {
                return likes;
            }

            set
            {
                likes = value;
            }
        }

        public int Dislikes
        {
            get
            {
                return dislikes;
            }

            set
            {
                dislikes = value;
            }
        }

        public Product(string NewName, string NewDescription, int NewPrice, string NewImageURL)
        {
            Name = NewName;
            Description = NewDescription;
            Price = NewPrice;
            ImageURL = NewImageURL;
        }
    }
}

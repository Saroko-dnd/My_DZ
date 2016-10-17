using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace OnlineStoreObjects
{
    [DataContract]
    public class Product
    {
        private int productID;
        private string imageURL;
        private string name;
        private string description = string.Empty;
        private int price;
        private int likes = 0;
        private int dislikes = 0;
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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

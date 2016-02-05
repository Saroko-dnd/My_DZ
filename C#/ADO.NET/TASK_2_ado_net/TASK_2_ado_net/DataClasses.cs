using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.Linq;

namespace TASK_2_ado_net
{
    class OurDataClass
    {
        private string ProductName_;
        private string LastName_;
        private string FirstName_;
        private string BirthDate_;
        private string HomePhone_;
        private string CompanyName_;
        private string ContactName_;
        private string Address_;
        private string City_;
        private string Country_;
        private string Phone_;
        private string RequiredDate_;
        private string ShippedDate_;
        private string Freight_;
        private string ShipCity_;
        private string ShipRegion_;
        private string ShipCountry_;
        private string Quantity_;
        private string UnitPrice_;
        private string Discount_;
        private BitmapImage Photo_;
        private string Discontinued_;
        private string QuantityPerUnit_;
        private string CategoryName_;

        public string CategoryName
        {
            get
            {
                return CategoryName_;
            }
            set
            {
                CategoryName_ = value;
            }
        }

        public string QuantityPerUnit
        {
            get
            {
                return QuantityPerUnit_;
            }
            set
            {
                QuantityPerUnit_ = value;
            }
        }

        public string Discontinued
        {
            get
            {
                if (Discontinued_ == "False")
                    return MyResourses.Texts.No;
                else
                    return MyResourses.Texts.Yes;

            }
            set
            {
                Discontinued_ = value;
            }
        }

        public string ProductName
        {
            get
            {
                return ProductName_;
            }
            set
            {
                ProductName_ = value;
            }
        }

        public string LastName
        {
            get
            {
                return LastName_;
            }
            set
            {
                LastName_ = value;
            }
        }

        public string FirstName
        {
            get
            {
                return FirstName_;
            }
            set
            {
                FirstName_ = value;
            }
        }

        public string BirthDate
        {
            get
            {
                return BirthDate_;
            }
            set
            {
                BirthDate_ = value;
            }
        }

        public string HomePhone
        {
            get
            {
                return HomePhone_;
            }
            set
            {
                HomePhone_ = value;
            }
        }

        public BitmapImage Photo
        {
            get
            {
                return Photo_;
            }
            set
            {
                Photo_ = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return CompanyName_;
            }
            set
            {
                CompanyName_ = value;
            }
        }

        public string ContactName
        {
            get
            {
                return ContactName_;
            }
            set
            {
                ContactName_ = value;
            }
        }

        public string Address
        {
            get
            {
                return Address_;
            }
            set
            {
                Address_ = value;
            }
        }

        public string City
        {
            get
            {
                return City_;
            }
            set
            {
                City_ = value;
            }
        }

        public string Country
        {
            get
            {
                return Country_;
            }
            set
            {
                Country_ = value;
            }
        }

        public string Phone
        {
            get
            {
                return Phone_;
            }
            set
            {
                Phone_ = value;
            }
        }

        public string RequiredDate
        {
            get
            {
                return RequiredDate_;
            }
            set
            {
                RequiredDate_ = value;
            }
        }

        public string ShippedDate
        {
            get
            {
                return ShippedDate_;
            }
            set
            {
                ShippedDate_ = value;
            }
        }

        public string Freight
        {
            get
            {
                return Freight_;
            }
            set
            {
                Freight_ = value;
            }
        }

        public string ShipCity
        {
            get
            {
                return ShipCity_;
            }
            set
            {
                ShipCity_ = value;
            }
        }

        public string ShipRegion
        {
            get
            {
                return ShipRegion_;
            }
            set
            {
                ShipRegion_ = value;
            }
        }

        public string ShipCountry
        {
            get
            {
                return ShipCountry_;
            }
            set
            {
                ShipCountry_ = value;
            }
        }

        public string Quantity
        {
            get
            {
                return Quantity_;
            }
            set
            {
                Quantity_ = value;
            }
        }

        public string UnitPrice
        {
            get
            {
                return UnitPrice_;
            }
            set
            {
                UnitPrice_ = value;
            }
        }

        public string Discount
        {
            get
            {
                return Discount_;
            }
            set
            {
                Discount_ = value;
            }
        }
    }
}

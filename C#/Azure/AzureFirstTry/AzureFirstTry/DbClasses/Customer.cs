using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFirstTry.DbClasses
{
    public class Customer
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int sumOfAllOrders { get; set; }
        [NotMapped]
        uint SumOfAllOrders 
        { 
            get
            {
                return (uint)sumOfAllOrders;
            } 
            set
            {
                sumOfAllOrders = (int)value;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreLogic
{
    public interface IRepository<DataType>
    {
        IEnumerable<DataType> GetAllData();
        IEnumerable<DataType> GetAllDataSortedByProperty(string PropertyName);
    }
}

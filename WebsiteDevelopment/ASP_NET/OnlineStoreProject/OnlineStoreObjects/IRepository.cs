using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreObjects
{
    public interface IRepository<DataType>
    {
        IEnumerable<DataType> GetAllData();
    }
}

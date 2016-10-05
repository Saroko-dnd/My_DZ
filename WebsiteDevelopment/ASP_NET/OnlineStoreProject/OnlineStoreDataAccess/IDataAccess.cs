using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreDataAccess
{
    public interface IDataAccess<DataType>
    {
        IEnumerable<DataType> LoadData();
    }
}

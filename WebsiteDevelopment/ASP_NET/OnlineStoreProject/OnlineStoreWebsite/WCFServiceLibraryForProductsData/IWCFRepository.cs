using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServiceLibraryForProductsData
{
    [ServiceContract]
    public interface IWCFRepository<DataType>
    {
        [OperationContract]
        IEnumerable<DataType> GetAllData();
    }
}

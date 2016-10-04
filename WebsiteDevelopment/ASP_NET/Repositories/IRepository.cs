using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Repositories
{
    interface IRepository<DataType>
    {
        IEnumerable<DataType> GetAllData();
    }
}

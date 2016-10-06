using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStoreObjects;
using System.Collections;

namespace OnlineStoreLogic
{
    public class LogicForManipulatingData<DataType>
    {
        private IRepository<DataType> CurrentRepository;
        public LogicForManipulatingData(IRepository<DataType> NewCurrentRepository)
        {
            CurrentRepository = NewCurrentRepository;
        }
    }
}

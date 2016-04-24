using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_OneWay_Reply
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IReply
    {
        [OperationContract(IsOneWay = true)]
        void FastReply();
        [OperationContract]
        void SlowReply();

        // TODO: Добавьте здесь операции служб
    }

}

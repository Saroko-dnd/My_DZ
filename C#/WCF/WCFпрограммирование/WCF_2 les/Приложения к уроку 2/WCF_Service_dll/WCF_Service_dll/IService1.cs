using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service_dll
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [DataContract]
    public class MathResult
    {
        [DataMember]
        public double sum;
        [DataMember]
        public double subtr;
        [DataMember]
        public double div;
        [DataMember]
        public double mult;
    }

    [ServiceContract]
    public interface IMyMath
    {
        [OperationContract]
        MathResult Total(int x, int y);
    }


    // Используйте контракт данных, как показано на следующем примере, чтобы добавить сложные типы к сервисным операциям.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "WCF_Service_dll.ContractType".
  
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_DuplexSvc
{
    /*это контракт службы, метод ReturnTime() может вызываться клиентом, при вызове клиент передает службе period – периодичность, с которой клиент хочет получать сообщения службы и number – количество этих сообщений. Чтобы не блокировать клиента, метод объявлен как односторонний. Обратите внимание на атрибут, который указывает, что контракт имеет callback составляющую*/

    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IDuplexSvc
    {
        [OperationContract(IsOneWay = true)]
        void ReturnTime(int period, int number);
    }
    /*это вторая часть контракта службы – контракт обратного вызова, метод ReceiveTime() описан на стороне клиента и может вызываться службой, при вызове служба передает клиенту информацию в string str*/

    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveTime(string str);
    }

}

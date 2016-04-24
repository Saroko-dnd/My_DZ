using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WCF_OneWay_Reply
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
        public class Reply: IReply
    	  {
        	public void FastReply()
        	{
            	Thread.Sleep(5000);
        	}
        	public void SlowReply()
        	{
            	Thread.Sleep(5000);
        	}
     }

 
}

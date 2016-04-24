using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF_AsyncClient.ServiceReference1;

namespace WCF_AsyncClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMathClient proxy = new MyMathClient();	//создаем объект прокси-класса
            IAsyncResult arAdd;	//готовим возвращаемое значение метода BeginTotal()

            //вызываем метод BeginTotal()
            //обратите внимание на 3-й и 4-й параметры:
            //GetSumCallback – адрес метода, который будет вызван по завершении //асинхронного вызова BeginTotal();
            //proxy – объект, передаваемый методу GetSumCallback()
            //через свойство AsyncState его параметра. Нам этот объект будет
            //нужем в методе GetSumCallback() для вызова EndTotal()

            arAdd = proxy.BeginTotal(100, 50, GetSumCallback, proxy);

            Console.WriteLine("Для завершения нажмите<ENTER>\n\n");
            Console.ReadLine();
        }
        //локальный метод, который будет вызван по завершении 
        //асинхронного вызова BeginTotal();
        static void GetSumCallback(IAsyncResult ar)
        {
            MathResult mr = ((MyMathClient)ar.AsyncState).EndTotal(ar);
            Console.WriteLine("Результат: {0} {1} {2} {3}", mr.sum, mr.subtr, mr.div,
            mr.mult);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadsSafetyExample_1
{
    class Program
    {     
        class worker
        {
            public static int n = 0;
            public static int n_2 = 0;
            public static object SuperLock = new object();
            public static object SuperLock_2 = new object();
            int i = 0;
            void func()
            {
                while (i != 10)
                {
                    //залочиваем FirstLock чтобы другие workers не могли 
                    //делать ++n пока его делает текущий экземпляр workera
                    lock (SuperLock)
                    {
                        ++n;
                    }
                    lock (SuperLock_2)
                    {
                        ++n_2;
                    }
                    
                    ++i;
                    //Console.WriteLine(n);
                }
            }
            public worker()
            {
                Thread thread = new Thread(func);
                thread.Start();
            }


        }
        static void Main(string[] args)
        {
            worker NewWorker_1 = new worker();
            worker NewWorker_2 = new worker();
            Console.ReadLine();
            Console.WriteLine(worker.n);
            //передаем параметр через лямбда функцию
            /*Thread FirstThread = new Thread(() => PrintIncrement(1));
            Thread SecondThread = new Thread(() => PrintIncrement(2));*/
            //приложение может быть завершено даже если Background поток продолжает работать
            /*FirstThread.IsBackground = true;
            FirstThread.Start();
            SecondThread.Start();*/
            Console.ReadLine();
        }

        public static void PrintIncrement(int num)
        {
            int countr = 0;
            while (countr != 10)
            {
                Console.WriteLine(countr.ToString() + " " + num.ToString());
                ++countr;
                Thread.Sleep(10);
            }
        }
    }
}

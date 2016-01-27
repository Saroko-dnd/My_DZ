using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace dynamic_loading_of_dll
{
    class Program
    {
        static void Main(string[] args)
        {
            //загружаем dll
            Assembly test_assembly = Assembly.Load("test_project");
            //получаем тип необходимого обьекта (test_project - namespace, test_class - класс)
            Type test_type = test_assembly.GetType("test_project.test_class");
            //(start)загрузка и использование конкретного метода обьекта
            Object instance = Activator.CreateInstance(test_type);
            //производим поиск и вызов необходимого нам крнструктора класса
            Type[] types = new Type[1];
            types[0] = typeof(int);
            ConstructorInfo constructor_of_instance = test_type.GetConstructor(types);
            constructor_of_instance.Invoke(instance, new object[] { 123 });
            //загрузка и использование конкретного метода обьекта
            MethodInfo necessary_method = test_type.GetMethod("print");
            necessary_method.Invoke(instance,new object[]{5});
            //(end)
            Console.ReadKey();
        }
    }
}

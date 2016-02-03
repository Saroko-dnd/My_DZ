using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example7Grouping
{
   class Program
    {
        static void Main(string[] args)
        {
           List<Employee> Empls = CreateList();

 #region     Группировка с использованием операторов запросов
           
            // Группировка данных по компании
            var GroupCompany= from p in Empls
                                group p by p.Company // указываем критерий (ключ) группировки 
                                into gr              // сохрание сгруппированных данных в gr
                                select               // формирование анонимного типа
                                new { 
                                    Company = gr.Key,   // создаем свойство и указываем ключ группировки
                                    Salary = gr.Sum(s => s.Salary) // суммируем значения в группе
                                    };

            foreach (var item in GroupCompany)
                Console.WriteLine(item);

            // Группировка данных по компании c формированием типизированной коллекции
            List<InfoCompany> GroupCompany2 = (from p in Empls
                                group p by p.Company
                                into gr  select 
                                new InfoCompany { Company = gr.Key, Salary = gr.Sum(p => p.Salary)}).ToList();

            foreach (InfoCompany item in GroupCompany2)
                Console.WriteLine(item);
            

            // Группировка по нескольким ключам 
            var GroupCompany3 = from p in Empls
                                group p by new { p.Company, p.StartDate }
                                into gr
                                select
                                new  {  Company = gr.Key.Company,
                                        Date = gr.Key.StartDate, 
                                        Salary = gr.Sum(p => p.Salary) 
                                        };

            foreach (var item in GroupCompany3)
                Console.WriteLine(item);

 #endregion


#region     Группировка с использованием расширяющих методов

            Console.WriteLine("Группировка с использованием расширяющих методов");

            // Группировка данных по компании
            var GroupCompany4 = Empls.GroupBy(p => p.Company).
                                    Select(gr => new { Company = gr.Key, Salary = gr.Sum(s => s.Salary) });

            foreach (var item in GroupCompany4)
                Console.WriteLine(item);

            List<InfoCompany> GroupCompany5 = (Empls.GroupBy(p => p.Company).
                                    Select(gr => new InfoCompany { Company = gr.Key, Salary = gr.Sum(s => s.Salary) })).ToList();

            foreach (InfoCompany item in GroupCompany5)
                Console.WriteLine(item);

            var GroupCompany6 = Empls.GroupBy(p => new {p.Company, p.StartDate}).
                                      Select(gr => new { Company = gr.Key, Date = gr.Key.StartDate, Salary = gr.Sum(s => s.Salary) });
            
            foreach (var item in GroupCompany6)
                Console.WriteLine(item);

#endregion

            Console.ReadKey();
        }
    

    static List<Employee> CreateList()
        {
            return new List<Employee>
            {
                new Employee
                {   FirstName = "Ivan", Company="ItProject", LastName = "Ivanov", Salary = 15000,
                    StartDate = DateTime.Parse("1/4/1992")
                },
                new Employee
                {
                    FirstName = "Petr", Company="EffectSoft", LastName = "Petrov",  Salary = 120000,
                    StartDate = DateTime.Parse("12/3/1985")
                },
                new Employee
                {
                    FirstName = "Andrew", Company="ItProject", LastName = "Andreev", Salary = 100000,
                    StartDate = DateTime.Parse("1/4/1992")
                }
            };

        }
    }

   public class InfoCompany
   {
       public string Company { get; set; }
       public decimal Salary { get; set; }

       public override string ToString()
       {
           return String.Format("{0} {1:C} ", Company, Salary);
       }
   }


    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}: {2} {3:C} {4}", FirstName, LastName, Company, Salary, StartDate);
        }
    }
}

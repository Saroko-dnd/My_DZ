using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example3LingObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> Empls = CreateList();

           
            Console.WriteLine("  Запрос на выборку ");
            var list1 = from employee in Empls where employee.Salary > 10000 select employee;
            foreach (var item in list1)
                Console.WriteLine(item);

                Console.WriteLine("  Запрос  на выборку со сложным критерием ");
            var list2 = from p in Empls where p.Salary > 10000  && p.FirstName.StartsWith("A")  select p;
            foreach (var item in list2)
                Console.WriteLine(item);

            Console.WriteLine("  Запрос на выборку и сортировка по возрастанию");
            var list3 = from p in Empls where p.Salary > 10000 orderby p.FirstName select p;
            foreach (var item in list3)
                Console.WriteLine(item);

            Console.WriteLine("  Запрос на выборку и сортировка по убыванию");
            var list4 = from p in Empls where p.Salary > 10000 orderby p.FirstName, p.Salary descending select p;
            foreach (var item in list4)
                Console.WriteLine(item);
            
            Console.WriteLine("  Запрос на выборку только фамилий ");
            var list5 = from p in Empls where p.Salary > 10000 select p.LastName;
            foreach (var item in list5)
                Console.WriteLine(item);

            Console.WriteLine("  Запрос на выборку и проецирование нового типа");
            var list6= from p in Empls where p.Salary > 10000 select new { p.LastName, p.StartDate };
            foreach (var item in list6)
                Console.WriteLine(item.LastName+": "+item.StartDate);


            Console.ReadKey();
        }


        static List<Employee> CreateList()
        {
            return new List<Employee>
            {
                new Employee
                {   FirstName = "Ivan",  LastName = "Ivanov", Salary = 94000,
                    StartDate = DateTime.Parse("1/4/1992")
                },
                new Employee
                {
                    FirstName = "Petr", LastName = "Petrov",  Salary = 123000,
                    StartDate = DateTime.Parse("12/3/1985")
                },
                new Employee
                {
                    FirstName = "Andrew", LastName = "Andreev", Salary = 1000000,
                    StartDate = DateTime.Parse("1/12/2005")
                }
            };
        
        }
    }

     public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}: {2:C} {3}", FirstName, LastName, Salary, StartDate);
        }
    }

}

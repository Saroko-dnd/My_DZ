using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example6Extension
{
    class Program
    {
        static void Main(string[] args)
        {
           List<Employee> Empls = CreateList();
           // Использование операций запросов C#
           var NewEmpls1  = from p in Empls where p.Salary>10000 select p.Salary;
           foreach (var item in NewEmpls1)
                Console.WriteLine(item);

           // Использование расширяющих методов класса Enumerable
           var NewEmpls2 = Empls.Where(p=>p.Salary>1000).Select(p=>p);
           foreach (var item in NewEmpls2)
               Console.WriteLine(item);


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

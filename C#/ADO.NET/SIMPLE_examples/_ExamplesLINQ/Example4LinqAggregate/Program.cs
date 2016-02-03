using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example4LinqAggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> Empls = CreateList();

            decimal Summa = (from p in Empls select p.Salary).Sum();
            Console.WriteLine("Сумма: {0}", Summa);
            
            decimal Average = (from p in Empls select p.Salary).Average();
            Console.WriteLine("Сумма: {0}", Average);
            
            decimal Max = (from p in Empls select p.Salary).Max();
            Console.WriteLine("Сумма: {0}", Max);
            
            decimal Min = (from p in Empls select p.Salary).Min();
            Console.WriteLine("Сумма: {0}", Min);

            int Count = (from p in Empls where p.Salary>10000 select p.Salary).Count();
            Console.WriteLine("Сумма: {0}", Count);

            var FirstTwo = (from p in Empls select p).Take(2);
            foreach (var item in FirstTwo)
            Console.WriteLine(item);

            var AfterTwo = (from p in Empls  select p).Skip(2);
            foreach (var item in AfterTwo)
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

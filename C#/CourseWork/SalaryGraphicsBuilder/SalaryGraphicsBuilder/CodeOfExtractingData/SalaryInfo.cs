using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryGraphicsBuilder.CodeOfExtractingData
{
    public class SalaryInfo
    {
        public int Salary;
        
        public SalaryInfo(string SalaryCurrency, double CurrentSalary)
        {
            switch (SalaryCurrency)
            {
                case "USD":
                    Salary = Convert.ToInt32(CurrentSalary * 1.9420);
                    break;
                case "RUR":
                    Salary = Convert.ToInt32(CurrentSalary * 0.0304);
                    break;
                case "BYR":
                    Salary = Convert.ToInt32(CurrentSalary);
                    break;
                case "EUR":
                    Salary = Convert.ToInt32(CurrentSalary * 2.1770);
                    break;
                case "UAH":
                    Salary = Convert.ToInt32(CurrentSalary * 0.0765);
                    break;
                default:
                    //Неизвестная валюта
                    Salary = -1;
                    break;
            }
        }

        public SalaryInfo()
        {
            Salary = -1;
        }
    }
}

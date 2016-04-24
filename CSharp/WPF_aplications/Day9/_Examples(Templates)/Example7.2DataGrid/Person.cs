using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example7._1DataGrid
{
    public class Person
    {
        public int EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public Department Department { get; set; }
        public DateTime Date { get; set; }
        public double Salory { get; set; }
        public bool GoodBoy { get; set; }

    }




    public enum Department
    {
        Programmers, 
        Managers,
        Disigners
    }

}

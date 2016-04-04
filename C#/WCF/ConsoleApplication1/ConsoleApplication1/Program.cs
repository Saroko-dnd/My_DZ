using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {


            DBAutoEntities db = new DBAutoEntities();
            TOwner owner = new TOwner()
            {
                Garanty = 50,
                Fam = "Ivanov",
                Date = DateTime.Now.ToShortDateString(),
                Opyt = 2
            };
            TMotor motor = new TMotor()
            {
                Marka = "VAZ",
                Country = "RB",
                Date = DateTime.Now.ToShortDateString(),
                M = 50
            };
            TAuto auto = new TAuto();
            auto.Country = "Germany";
            auto.Date = DateTime.Now.ToShortDateString();
            auto.R = 20;
            auto.Marka = "BMW";
            auto.V = 500;
            auto.TMotor = motor;
            auto.TOwner.Add(owner);
            db.TAuto.Add(auto);
            db.SaveChanges();
        }
    }

}
using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.Areas.Admin.Models
{
    public class RaceAdminModel
    {
        public bool RaceHasNotStartedYet {get; set;}
        public IEnumerable<Car> ListOfCars {get; set;}

        public RaceAdminModel(IEnumerable<Car> NewListOfCars, bool RaceHasNotYetBegun)
        {
            ListOfCars = NewListOfCars;
            RaceHasNotStartedYet = RaceHasNotYetBegun;
        }
    }
}
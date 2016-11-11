using RaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.Areas.Admin.Models
{
    public class RaceAdminModel
    {
        public IEnumerable<Car> ListOfCars {get; set;}
    }
}
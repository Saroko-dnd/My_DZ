using Ninject;
using RaceDataAccess;
using RaceInfrastructure;
using RaceLogic;
using RaceWebsite.ClassesForRaceWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceWebsite.App_Start
{
    public class UsualNinjectConfigForNoncontrollers
    {
        public static IKernel DependencyResolver;

        public static void ConfigureUsualNinject()
        {
            DependencyResolver = new StandardKernel();

            DependencyResolver.Bind<IRaceRepository>().To<RaceWebsiteDbContext>().InThreadScope().WithConstructorArgument("ConnectionStringName", ApplicationConstants.ConnectionStringName);
            DependencyResolver.Bind<IRaceManager>().To<RaceManager>().InSingletonScope();
        }
    }
}
using Ninject;
using Ninject.Web.Common;
using RaceDataAccess;
using RaceInfrastructure;
using RaceLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaceWebsite.ClassesForRaceWebsite
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IRaceRepository>().To<RaceWebsiteDbContext>().InThreadScope().WithConstructorArgument("ConnectionStringName", ApplicationConstants.ConnectionStringName);
            kernel.Bind<IRaceManager>().To<RaceManager>();
            kernel.Bind<IAccessorToRaceInfo>().To<AccessorToRaceInfo>().InSingletonScope();
        }
    }
}
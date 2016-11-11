using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Threading.Tasks;
using NewsInfrastructure;
using NewsDataAccess;
using NewsLogic;
using Ninject.Web.Common;

namespace NewsWebsite.ClassesForNewsWebsite
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
            kernel.Bind<INewsWebsiteRepository>().To<NewsWebsiteContext>().InRequestScope().WithConstructorArgument("ConnectionStringName", ApplicationConstants.ConnectionStringName);
            kernel.Bind<INewsWebsiteDataManager>().To<NewsWebsiteDataManager>();
        }
    }
}
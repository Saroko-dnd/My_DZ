
using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using NewsInfrastructure;
using NewsDataAccess;
using NewsWebsite.ClassesForNewsWebsite;
using NewsLogic;

namespace NewsWebsite.App_Start
{


    public static class NinjectWebCommon 
    {
        public static IKernel NinjectKernel;

        public static void ConfigureNinject()
        {
            NinjectKernel = new StandardKernel();
            NinjectKernel.Bind<INewsWebsiteRepository>().To<NewsWebsiteContext>().WithConstructorArgument("ConnectionStringName", ApplicationConstants.ConnectionStringName);
            NinjectKernel.Bind<INewsWebsiteDataManager>().To<NewsWebsiteDataManager>().WithConstructorArgument("NewKernelWithRepositoryBinding", NinjectKernel);
        }
    }
}

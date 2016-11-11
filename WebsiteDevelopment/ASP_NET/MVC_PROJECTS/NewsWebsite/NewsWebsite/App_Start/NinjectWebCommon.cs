
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NewsWebsite.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NewsWebsite.App_Start.NinjectWebCommon), "Stop")]

namespace NewsWebsite.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Web.Mvc;
    using System.Web.Http;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            NewsWebsite.ClassesForNewsWebsite.NinjectDependencyResolver CurrentNinjectDependencyResolver = new NewsWebsite.ClassesForNewsWebsite.NinjectDependencyResolver(kernel);
            DependencyResolver.SetResolver(CurrentNinjectDependencyResolver);
        }
    }
}  

/*namespace NewsWebsite.App_Start
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
} */

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(JobPortal.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(JobPortal.Web.App_Start.NinjectWebCommon), "Stop")]

namespace JobPortal.Web.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi;

    using Ninject.Web.Common.WebHost;
    using AutoMapper;
    using JobPortal.Web.App_Start.AutoMapperProfiles;
    using JobPortal.Business.Core.Contract;
    using JobPortal.Business.Service;
    using JobPortal.DataAccess.Core.Contract;
    using JobPortal.DataAccess.Repository;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
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

        private static void RegisterAutoMapper(IKernel kernel)
        {
            kernel.Bind<IMapper>().ToMethod((ctx) =>
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile<WebBusinessProfile>();
                    cfg.AddProfile<BusinessDataAccessProfile>();
                });
                var mapper = new Mapper(config);
                return mapper;
            }).InSingletonScope();
        }

        private static void RegisterBusinessServices(IKernel kernel)
        {
            kernel.Bind<IJobPostService>().To<JobPostService>().InSingletonScope();
        }

        private static void RegisterDataAccessServices(IKernel kernel)
        {
            kernel.Bind<IJobPostRepository>().To<JobPostRepository>().InSingletonScope();
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            RegisterAutoMapper(kernel);
            RegisterBusinessServices(kernel);
            RegisterDataAccessServices(kernel);
        }        
    }
}

using System;
using System.Web;
using System.Web.Http;
using FanSelector.Api;
using FanSelector.Core;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using WebActivatorEx;
using Ninject.Extensions.Conventions;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace FanSelector.Api
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x => x
                .FromAssembliesMatching("FanSelector.Core.dll", "FanSelector.Api.dll")
                .SelectAllClasses()
                .BindDefaultInterface());

            kernel.Bind(x => x
                .FromAssembliesMatching("FanSelector.Data.QueryService.dll")
                .SelectAllClasses().InheritedFrom(typeof(IRepository))
                .BindAllInterfaces());

            kernel.Bind(x => x
                .FromAssembliesMatching("FanSelector.Data.CommandService.dll")
                .SelectAllClasses().InheritedFrom(typeof(IRepository))
                .BindAllInterfaces());

            kernel.Bind(x => x
                .FromAssembliesMatching("FanSelector.Data.Core.dll")
                .SelectAllClasses().InheritedFrom(typeof(IQueryHandler<,>))
                .BindAllInterfaces());

            kernel.Bind(x => x
                .FromAssembliesMatching("FanSelector.Data.Core.dll")
                .SelectAllClasses().InheritedFrom(typeof(ICommandHandler<>))
                .BindAllInterfaces());
        }
    }
}
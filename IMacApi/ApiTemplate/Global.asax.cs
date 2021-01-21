using ApiTemplate.Commons.Helpers;
using ApiTemplate.Modules;
using Autofac;
using Autofac.Integration.WebApi;
using AutofacSerilogIntegration;
using Serilog;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web.Http;

namespace ApiTemplate
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RunLogger();
            RegisterAutofac();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.Add(new BrowserJsonFormatter());
            
            //JobScheduler.Start();
        }

        private void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterLogger();
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver
        }

        private void RunLogger()
        {
            //var basedir = AppDomain.CurrentDomain.BaseDirectory;
            //var basedir = "D:\\Logs\\API\\IMACMOBILE";
            var basedir = ConfigurationManager.AppSettings["Logerror"];

            if (!Directory.Exists(basedir))
                Directory.CreateDirectory(basedir);

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.File($"{basedir}\\Logs\\log.txt", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File($"{basedir}\\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Server starting ...");


        }
    }
}

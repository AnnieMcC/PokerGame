using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using PokerHandSorter.Services;
using Serilog;

namespace PokerHandSorter
{
    public class PokerHandConfiguration
    {
        public static IContainer Container;
        public static IConfiguration Configuration { get; set; }

        internal static IContainer Configure()
        {
            var configBuilder = new ConfigurationBuilder();
            Configuration = configBuilder.Build();

            //serilog
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] - {Message}{NewLine}{Exception}")
            //    .CreateLogger();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PokerGameService>().As<IPokerGameService>();
            //containerBuilder.RegisterType<DataLayerService>().As<IDataLayerService>();
            //containerBuilder.RegisterType<JsonFileDataService>().As<IJsonFileDataService>();

           // containerBuilder.RegisterType<SerilogService>().As<ILoggerService>();

            var builder = containerBuilder.Build();
            Container = builder;

            return builder;
        }
    }
}

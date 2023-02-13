using Alfred.Messages;
using Alfred.Plugins;
using Alfred.Sensors;
using AlfredUtilities.Messages;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

using System;

namespace Alfred
{
    internal static class Program
    {
        private static void Main()
        {
            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                services.AddSingleton<IPluginPathFinder, PluginPathFinder>()
                .AddSingleton<IPluginStore, PluginStore>()
                .AddSingleton<ISensorService, SimpleSensorService>()
                .AddSingleton<IMessageDispatcher, MessageDispatcher>()
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<MainApp>()
                .AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog();
                }))                
                .Build();

            IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();
            string toto = configuration.GetValue<string>("PluginsPath");
            
            IServiceScope alfredMainScope = host.Services.CreateScope();
            IServiceProvider provider = alfredMainScope.ServiceProvider;

            using MainApp mainApp = provider.GetRequiredService<MainApp>().Init();

            mainApp.Run();
        }
    }
}
using Alfred.Messages;
using Alfred.Plugins;
using Alfred.Sensors;
using AlfredUtilities.Messages;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace SuperBack
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
                .AddSingleton<MainApp>())
                .Build();


            IServiceScope alfredMainScope = host.Services.CreateScope();
            IServiceProvider provider = alfredMainScope.ServiceProvider;

            using MainApp mainApp = provider.GetRequiredService<MainApp>().Init();

            Console.WriteLine("Alfred says : Welcome master.");
            mainApp.Run();
        }
    }
}
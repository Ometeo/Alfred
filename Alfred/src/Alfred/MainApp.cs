using AlfredPlugin;
using AlfredUtilities;
using Autofac;
using SuperBack.Plugins;
using SuperBack.Sensor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SuperBack
{
    public class MainApp : AlfredBase
    {
        private ContainerBuilder containerBuilder = null;

        public IContainer Value { get; private set; }

        public ISensorService SensorService { get; private set; }
        ILifetimeScope scope;

        string[] pluginPaths = new string[]
        {
            @"I:\Projects\DIY\HomeSupervisor\HomeSupervisor\SuperBack\FakePlugin\bin\Debug\netcoreapp3.0\FakePlugin.dll"
        };

        public MainApp()
        {
            Console.WriteLine("* Create Alfred main app.");
            containerBuilder = new ContainerBuilder();
        }

        public MainApp Init()
        {
            Console.WriteLine("* Init Alfred.");
            containerBuilder.RegisterType<PluginPathFinder>().As<IPluginPathFinder>();
            containerBuilder.RegisterType<PluginStore>().As<IPluginStore>();
            containerBuilder.RegisterType<SimpleSensorService>().As<ISensorService>();


            Console.WriteLine("* Build Alfred DI container.");
            Value = containerBuilder.Build();

            scope = Value.BeginLifetimeScope();
            Console.WriteLine("* Create Alfred sensor service.");
            SensorService = scope.Resolve<ISensorService>();

            Console.WriteLine("* LoadPlugins.");
            var pluginStore = scope.Resolve<IPluginStore>();
            pluginStore.LoadPlugins();           

            return this;
        }

        public void Run()
        {
                
        }

        protected override void DisposeManagedObjects()
        {
            Console.WriteLine("* Dispose Managed objects in main app");
            Value.Dispose();
            scope.Dispose();
        }

        protected override void DisposeUnmanagedObjects()
        {
            Console.WriteLine("* Dispose Unmanaged objects in main app");
        }
    }
}

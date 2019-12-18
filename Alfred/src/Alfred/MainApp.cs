using Alfred.Messages;
using Alfred.Sensors;
using AlfredUtilities;
using AlfredUtilities.Messages;
using Autofac;
using SuperBack.Plugins;
using System;

namespace SuperBack
{
    public class MainApp : AlfredBase, IMessageListener
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
            containerBuilder.RegisterType<PluginPathFinder>().As<IPluginPathFinder>().SingleInstance();
            containerBuilder.RegisterType<PluginStore>().As<IPluginStore>().SingleInstance();
            containerBuilder.RegisterType<SimpleSensorService>().As<ISensorService>().SingleInstance();
            containerBuilder.RegisterType<MessageDispatcher>().As<IMessageDispatcher>().SingleInstance();
          
            Console.WriteLine("* Build Alfred DI container.");
            Value = containerBuilder.Build();

            scope = Value.BeginLifetimeScope();

            IMessageDispatcher dispatcher = scope.Resolve<IMessageDispatcher>();

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

        public void Consume(Message message)
        {
            Console.WriteLine($"* Receive message on main app : {message.Topic}, {message.Content}");
        }
    }
}

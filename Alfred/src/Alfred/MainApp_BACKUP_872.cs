using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alfred.Messages;
using Alfred.Plugins;
using Alfred.Sensors;

using AlfredFrontInterface;

using AlfredUtilities;
using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

using Autofac;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Alfred
{
    public class MainApp : AlfredBase, IMessageListener
    {
        #region Private Fields

        private readonly ContainerBuilder containerBuilder = null;

        private ILifetimeScope scope;

        #endregion Private Fields

        #region Public Constructors

        public MainApp()
        {
            Console.WriteLine("* Create Alfred main app.");
            containerBuilder = new ContainerBuilder();
        }

        #endregion Public Constructors

        #region Public Properties

        public ISensorService SensorService
        {
            get; private set;
        }

        public IContainer Value
        {
            get; private set;
        }

        #endregion Public Properties

        #region Internal Properties

        internal IPluginStore PluginStore
        {
            get; private set;
        }

        internal IFrontCommunicator<Sensor> FrontCommunicator
        {
            get;
            private set;
        }
        #endregion Internal Properties

        #region Public Methods

        public void Consume(Message message)
        {
            Console.WriteLine($"* Receive message on main app : {message.Topic}, {message.Content}");
        }

        public MainApp Init()
        {
            Console.WriteLine("* Init Alfred.");
<<<<<<< Updated upstream
            containerBuilder.RegisterType<PluginPathFinder>().As<IPluginPathFinder>().SingleInstance();
            containerBuilder.RegisterType<PluginStore>().As<IPluginStore>().SingleInstance();
            containerBuilder.RegisterType<SimpleSensorService>().As<ISensorService>().SingleInstance();
            containerBuilder.RegisterType<MessageDispatcher>().As<IMessageDispatcher>().SingleInstance();
=======
            _ = containerBuilder.RegisterType<PluginPathFinder>().As<IPluginPathFinder>().SingleInstance();
            _ = containerBuilder.RegisterType<PluginStore>().As<IPluginStore>().SingleInstance();
            _ = containerBuilder.RegisterType<SimpleSensorService>().As<ISensorService>().SingleInstance();
            _ = containerBuilder.RegisterType<MessageDispatcher>().As<IMessageDispatcher>().SingleInstance();
            _ = containerBuilder.RegisterType(typeof(RabbitMQFrontCommunicator)).As(typeof(IFrontCommunicator<Sensor>)).SingleInstance();
>>>>>>> Stashed changes

            Console.WriteLine("* Build Alfred DI container.");
            Value = containerBuilder.Build();

            scope = Value.BeginLifetimeScope();

            FrontCommunicator = scope.Resolve<IFrontCommunicator<Sensor>>();

            IMessageDispatcher dispatcher = scope.Resolve<IMessageDispatcher>();

            Console.WriteLine("* Create Alfred sensor service.");
            SensorService = scope.Resolve<ISensorService>();
            SensorService.SensorChanged += SensorService_SensorChanged;

            Console.WriteLine("* LoadPlugins.");
            PluginStore = scope.Resolve<IPluginStore>();
            PluginStore.LoadPlugins();

            IEnumerable<string> pluginsNames = PluginStore.PluginsName();
            Console.WriteLine("* Plugins Loaded:");
            pluginsNames.ToList().ForEach(name => Console.WriteLine($"    - {name}"));
           
            return this;
        }

        private void SensorService_SensorChanged(object sender, Sensor e)
        {
            FrontCommunicator.Send(e);
        }

        public void Run()
        {
            while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Q)
            {
                PluginStore.Plugins.ToList().ForEach(plugin => plugin.Update());
            }
        }

        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods
    }
}
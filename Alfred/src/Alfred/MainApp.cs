using System;
using System.Collections.Generic;
using System.Linq;
using Alfred.Messages;
using Alfred.Plugins;
using Alfred.Sensors;

using AlfredUtilities;
using AlfredUtilities.Messages;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SuperBack
{
    public class MainApp : AlfredBase, IMessageListener
    {
        #region Private Fields

        private readonly string[] pluginPaths = new string[]
        {
            @"I:\Projects\DIY\HomeSupervisor\HomeSupervisor\SuperBack\FakePlugin\bin\Debug\netcoreapp3.0\FakePlugin.dll"
        };

        #endregion Private Fields

        #region Public Constructors

        public MainApp(ISensorService sensorService, IMessageDispatcher dispatcher, IPluginStore pluginStore)
        {
            Console.WriteLine("* Create Alfred main app.");
            Console.WriteLine("* Create Alfred sensor service.");
            SensorService = sensorService;

            Console.WriteLine("* Create Plugin Store.");
            PluginStore = pluginStore;
            Console.WriteLine("* Create Message Dispatcher.");
            Dispatcher = dispatcher;
        }

        #endregion Public Constructors

        #region Public Properties

        public ISensorService SensorService
        {
            get; private set;
        }

        #endregion Public Properties

        #region Internal Properties

        internal IPluginStore PluginStore
        {
            get; private set;
        }

        internal IMessageDispatcher Dispatcher
        {
            get; private set;
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
            Console.WriteLine("* LoadPlugins.");
            PluginStore.LoadPlugins();

            IEnumerable<string> pluginsNames = PluginStore.PluginsName();
            Console.WriteLine("* Plugins Loaded:");
            pluginsNames.ToList().ForEach(name => Console.WriteLine($"    - {name}"));

            return this;
        }

        public void Run()
        {
            //while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Q)
            //{
            //    Console.WriteLine(".");
            //}
            PluginStore.Plugins.ToList().ForEach(plugin => plugin.Update());
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void DisposeManagedObjects()
        {
            Console.WriteLine("* Dispose Managed objects in main app");        
        }

        protected override void DisposeUnmanagedObjects()
        {
            Console.WriteLine("* Dispose Unmanaged objects in main app");
        }

        #endregion Protected Methods
    }
}
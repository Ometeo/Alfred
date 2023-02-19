using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

using Alfred.Plugins;
using Alfred.Sensors;
using AlfredUtilities;
using AlfredUtilities.Messages;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace Alfred
{
    public class MainApp : AlfredBase, IHostedService, IMessageListener
    {
        #region Private Fields

        private readonly ILogger _logger;

        #endregion Private Fields

        #region Public Constructors

        public MainApp(ISensorService sensorService, IMessageDispatcher dispatcher, IPluginStore pluginStore, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MainApp>();
            
            _logger.LogInformation("* Create Alfred main app.");

            _logger.LogInformation("* Create Alfred sensor service.");
            SensorService = sensorService;

            _logger.LogInformation("* Create Plugin Store.");
            PluginStore = pluginStore;
            _logger.LogInformation("* Create Message Dispatcher.");
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
            _logger.LogTrace($"* Receive message on main app : {message.Topic}, {message.Content}");
        }

        public MainApp Init()
        {
            _logger.LogInformation("* Init Alfred.");                                             
            _logger.LogInformation("* LoadPlugins.");
            PluginStore.LoadPlugins();

            IEnumerable<string> pluginsNames = PluginStore.PluginsName();
            _logger.LogInformation("* Plugins Loaded:");
            pluginsNames.ToList().ForEach(name => _logger.LogInformation($"    - {name}"));

            return this;
        }

        public void Run()
        {
            //while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Q)
            //{
            //    _logger.LogInformation(".");
            //}
            PluginStore.Plugins.ToList().ForEach(plugin => plugin.Update());
        }

        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start main service");
            Init();
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


        private void DoWork(object state)
        {
            Run();
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void DisposeManagedObjects()
        {
            _logger.LogInformation("* Dispose Managed objects in main app");    
            _timer.Dispose();
        }

        protected override void DisposeUnmanagedObjects()
        {
            _logger.LogInformation("* Dispose Unmanaged objects in main app");
        }

        #endregion Protected Methods
    }
}

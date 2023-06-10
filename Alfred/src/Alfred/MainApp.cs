using Alfred.Plugins;
using Alfred.SensorsService;

using AlfredUtilities;
using AlfredUtilities.Messages;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alfred
{
    public class MainApp : AlfredBase, IHostedService, IMessageListener
    {
        #region Private Fields

        private readonly ILogger _logger;
        private readonly uint _period = 5000;
        private readonly Timer _timer;

        #endregion Private Fields

        #region Public Constructors

        public MainApp(ISensorsService sensorService, IMessageDispatcher dispatcher, IPluginStore pluginStore, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MainApp>();

            _logger.LogInformation("* Create Alfred main app.");

            _logger.LogInformation("* Create Alfred sensor service.");
            SensorService = sensorService;

            _logger.LogInformation("* Create Plugin Store.");
            PluginStore = pluginStore;
            _logger.LogInformation("* Create Message Dispatcher.");
            Dispatcher = dispatcher;
            _logger.LogInformation("* Init main app timer.");
            _timer = new Timer(Run, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_period));
            _ = _timer.Change(Timeout.Infinite, 0);
        }

        #endregion Public Constructors

        #region Public Properties

        public ISensorsService SensorService
        {
            get; private set;
        }

        #endregion Public Properties

        #region Internal Properties

        internal IMessageDispatcher Dispatcher
        {
            get; private set;
        }

        internal IPluginStore PluginStore
        {
            get; private set;
        }

        #endregion Internal Properties

        #region Public Methods

        public void Consume(Message message)
        {
            _logger.LogTrace("* Receive message on main app : {}, {}", message.Topic, message.Content);
        }

        public MainApp Init()
        {
            _logger.LogInformation("* Init Alfred.");
            _logger.LogInformation("* LoadPlugins.");
            PluginStore.LoadPlugins();

            IEnumerable<string> pluginsNames = PluginStore.PluginsName();
            _logger.LogInformation("* Plugins Loaded:");
            pluginsNames.ToList().ForEach(name => _logger.LogInformation("    - {}", name));

            return this;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start main service");
            _ = Init();
            _ = _timer.Change(0, _period);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _ = _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void Run(object? state)
        {
            PluginStore.Plugins.ToList().ForEach(plugin => plugin.Update());
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

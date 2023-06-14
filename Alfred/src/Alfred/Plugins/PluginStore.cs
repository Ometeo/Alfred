using AlfredPlugin;

using AlfredUtilities;
using AlfredUtilities.Messages;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace Alfred.Plugins
{
    internal class PluginStore : AlfredBase, IPluginStore
    {
        #region Private Fields

        private readonly ILogger _logger;
        private readonly IMessageDispatcher messageDispatcher;
        private readonly IList<string> pluginsPath;
        private IEnumerable<IAlfredPlugin> plugins = new List<IAlfredPlugin>();

        #endregion Private Fields

        #region Public Constructors

        public PluginStore(IPluginPathFinder pluginPathFinder, IMessageDispatcher messageDispatcher, ILoggerFactory loggerFactory)
        {
            ArgumentNullException.ThrowIfNull(pluginPathFinder);
            ArgumentNullException.ThrowIfNull(messageDispatcher);
            ArgumentNullException.ThrowIfNull(loggerFactory);

            _logger = loggerFactory.CreateLogger<PluginStore>();
            pluginsPath = pluginPathFinder.PluginPaths();
            this.messageDispatcher = messageDispatcher;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Todo copy list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAlfredPlugin> IPluginStore.Plugins
        {
            get
            {
                return plugins;
            }
        }

        public void LoadPlugins()
        {
            plugins = pluginsPath.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreatePlugin(pluginAssembly);
            }).ToList();
        }

        public IEnumerable<string> PluginsName()
        {
            IEnumerable<string> toto = plugins.Select(plugin => plugin.Name);
            return toto;
        }

        #endregion Public Methods

        #region Protected Methods

        protected static Assembly LoadPlugin(string pluginPath)
        {
            string pluginLocation = Path.GetFullPath(pluginPath.Replace('\\', Path.DirectorySeparatorChar));
            AssemblyLoadContext loadContext = new(pluginLocation);
            return loadContext.LoadFromAssemblyPath(pluginLocation);
        }

        protected IEnumerable<IAlfredPlugin> CreatePlugin(Assembly pluginAssembly)
        {
            int count = 0;

            foreach (Type type in pluginAssembly.GetTypes())
            {
                if (typeof(IAlfredPlugin).IsAssignableFrom(type) && Activator.CreateInstance(type) is IAlfredPlugin result)
                {
                    result.Init(messageDispatcher);
                    ++count;
                    yield return result;
                }
            }

            if (0 == count)
            {
                throw new PluginNotFoundException(
                    $"AlfredPlugin not found in {pluginAssembly}");
            }
        }

        protected override void DisposeManagedObjects()
        {
            _logger.LogInformation("    * Dispose Managed Objects in PluginStore");
        }

        protected override void DisposeUnmanagedObjects()
        {
            _logger.LogInformation("    * Dispose Unmanaged Objects in PluginStore");
        }

        #endregion Protected Methods
    }
}

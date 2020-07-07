using AlfredPlugin;
using AlfredUtilities;
using AlfredUtilities.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Alfred.Plugins
{
    internal class PluginStore : AlfredBase, IPluginStore
    {
        #region Private Fields

        private readonly IMessageDispatcher messageDispatcher;
        private readonly IList<string> pluginsPath;
        private IEnumerable<IAlfredPlugin> plugins;

        #endregion Private Fields

        #region Public Constructors

        public PluginStore(IPluginPathFinder pluginPathFinder, IMessageDispatcher messageDispatcher)
        {
            pluginsPath = pluginPathFinder.PluginPaths();
            this.messageDispatcher = messageDispatcher;
        }

        #endregion Public Constructors

        #region Public Methods

        public void LoadPlugins()
        {
            plugins = pluginsPath.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreatePlugin(pluginAssembly);
            }).ToList();
        }

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

        public IEnumerable<string> PluginsName()
        {
            IEnumerable<string> toto = plugins.Select(plugin => plugin.Name);
            return toto;
        }

        #endregion Public Methods

        #region Protected Methods

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
            Console.WriteLine("    * Dispose Managed Objects in PluginStore");
        }

        protected override void DisposeUnmanagedObjects()
        {
            Console.WriteLine("    * Dispose Unmanaged Objects in PluginStore");
        }

        protected Assembly LoadPlugin(string pluginPath)
        {
            string pluginLocation = Path.GetFullPath(pluginPath.Replace('\\', Path.DirectorySeparatorChar));
            AssemblyLoadContext loadContext = new AssemblyLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyPath(pluginLocation);
        }

        #endregion Protected Methods
    }
}
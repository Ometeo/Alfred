using AlfredPlugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SuperBack.Plugins
{
    internal class PluginStore : IPluginStore
    {
        IEnumerable<IAlfredPlugin> plugins;
        IList<string> pluginsPath;

        public PluginStore(IPluginPathFinder pluginPathFinder)
        {
            pluginsPath = pluginPathFinder.PluginPaths();
        }

        public void LoadPlugins()
        {
            plugins = pluginsPath.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreatePlugin(pluginAssembly);
            }).ToList();
        }

        protected IEnumerable<IAlfredPlugin> CreatePlugin(Assembly pluginAssembly)
        {
            int count = 0;

            foreach (Type type in pluginAssembly.GetTypes())
            {
                if (typeof(IAlfredPlugin).IsAssignableFrom(type))
                {
                    IAlfredPlugin result = Activator.CreateInstance(type) as IAlfredPlugin;
                    if (null != result)
                    {
                        ++count;
                        yield return result;
                    }
                }
            }

            if (0 == count)
            {
                string availableTypes = string.Join(",", pluginAssembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"AlfredPlugin not found in {pluginAssembly}");
            }
        }

        protected Assembly LoadPlugin(string pluginPath)
        {
            string pluginLocation = Path.GetFullPath(pluginPath.Replace('\\', Path.DirectorySeparatorChar));
            AssemblyLoadContext loadContext = new AssemblyLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyPath(pluginLocation);
        }

        /// <summary>
        /// Todo copy list
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAlfredPlugin> IPluginStore.Plugins()
        {
            return plugins;
        }
    }
}

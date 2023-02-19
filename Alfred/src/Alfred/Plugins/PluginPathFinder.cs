using AlfredUtilities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Alfred.Plugins
{
    internal class PluginPathFinder : AlfredBase, IPluginPathFinder
    {
        private readonly IList<string> paths = new List<string>();

        private readonly ILogger _logger;

        public PluginPathFinder(ILoggerFactory loggerFactory, IConfiguration config)
        {            
            _logger = loggerFactory.CreateLogger<PluginPathFinder>();
            string basePath = config.GetValue<string>("PluginsPath") ?? string.Empty;
            string executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string executingDirectory = Path.GetDirectoryName(executingAssemblyPath) ?? ".";
            string pluginsPath = Path.Combine(executingDirectory, basePath);
            if (Directory.Exists(pluginsPath))
            {
                foreach (string pluginPath in Directory.GetFiles(pluginsPath).Where(path => Path.GetExtension(path).Equals(".dll")))
                {
                    paths.Add(Path.GetFullPath(pluginPath));
                }
            }
        }

        public IList<string> PluginPaths()
        {
            return paths;
        }

        protected override void DisposeManagedObjects()
        {
            _logger.LogInformation("    * Dispose Managed Objects in PluginPathFinder");
        }

        protected override void DisposeUnmanagedObjects()
        {
            _logger.LogInformation("    * Dispose Unmanaged Objects in PluginPathFinder");
        }
    }
}

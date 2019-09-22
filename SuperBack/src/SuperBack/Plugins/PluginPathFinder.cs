using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace SuperBack.Plugins
{
    internal class PluginPathFinder : IPluginPathFinder
    {
        IList<string> paths = new List<string>();

        public PluginPathFinder()
        {
            string basePath = ConfigurationManager.AppSettings["pluginsPath"];
            if(Directory.Exists(basePath))
            {
                foreach (string pluginPath in Directory.GetFiles(basePath).Where(path => Path.GetExtension(path).Equals(".dll")))
                {
                    paths.Add(Path.GetFullPath(pluginPath));
                }
            }
        }

        public IList<string> PluginPaths()
        {
            return paths;
        }
    }
}

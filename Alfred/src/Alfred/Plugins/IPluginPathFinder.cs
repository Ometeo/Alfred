using System.Collections.Generic;

namespace SuperBack.Plugins
{
    internal interface IPluginPathFinder
    {
        IList<string> PluginPaths();
    }
}

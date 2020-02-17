using System.Collections.Generic;

namespace Alfred.Plugins
{
    internal interface IPluginPathFinder
    {
        IList<string> PluginPaths();
    }
}
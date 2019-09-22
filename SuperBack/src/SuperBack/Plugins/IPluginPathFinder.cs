using System;
using System.Collections.Generic;
using System.Text;

namespace SuperBack.Plugins
{
    internal interface IPluginPathFinder
    {
        IList<string> PluginPaths();
    }
}

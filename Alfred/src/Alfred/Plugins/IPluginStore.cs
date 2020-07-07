using System.Collections.Generic;

using AlfredPlugin;

namespace Alfred.Plugins
{
    internal interface IPluginStore
    {
        void LoadPlugins();

        IEnumerable<IAlfredPlugin> Plugins
        {
            get;
        }

        IEnumerable<string> PluginsName();
    }
}
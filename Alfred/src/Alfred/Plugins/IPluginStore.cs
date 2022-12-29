using System.Collections.Generic;

using AlfredPlugin;

namespace Alfred.Plugins
{
    public interface IPluginStore
    {
        void LoadPlugins();

        IEnumerable<IAlfredPlugin> Plugins
        {
            get;
        }

        IEnumerable<string> PluginsName();
    }
}
using AlfredPlugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperBack.Plugins
{
    internal interface IPluginStore
    {        
        void LoadPlugins();

        IEnumerable<IAlfredPlugin> Plugins();
    }
}

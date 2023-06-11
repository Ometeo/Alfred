using AlfredPlugin;

using System.Collections.Generic;

namespace Alfred.Plugins
{
    public interface IPluginStore
    {
        #region Public Properties

        IEnumerable<IAlfredPlugin> Plugins
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods

        void LoadPlugins();

        IEnumerable<string> PluginsName();

        #endregion Public Methods
    }
}

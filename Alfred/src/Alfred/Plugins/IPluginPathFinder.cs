using System.Collections.Generic;

namespace Alfred.Plugins
{
    internal interface IPluginPathFinder
    {
        #region Public Methods

        IList<string> PluginPaths();

        #endregion Public Methods
    }
}

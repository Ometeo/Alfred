using System;
using System.Runtime.Serialization;

namespace Alfred.Plugins
{
    [Serializable]
    public class PluginNotFoundException : Exception
    {
        #region Public Constructors

        public PluginNotFoundException()
        {
        }

        public PluginNotFoundException(string message) : base(message)
        {
        }

        public PluginNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected PluginNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }

        #endregion Protected Constructors
    }
}

using System;
using System.Runtime.Serialization;

namespace Alfred.Plugins
{
    [Serializable]
    public class PluginNotFoundException : Exception
    {
        public PluginNotFoundException()
        {
        }

        public PluginNotFoundException(string message) : base(message)
        {
        }

        public PluginNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PluginNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
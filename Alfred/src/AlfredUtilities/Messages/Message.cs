namespace AlfredUtilities.Messages
{
    /// <summary>
    /// Message for message dispatcher.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Topic of message.
        /// <para>Used for dispatch message to the correct listener.</para>
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Content of the message.
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Null message.
        /// </summary>
        public static readonly Message Null = new NullMessage();
        private class NullMessage : Message
        {
            public NullMessage() : base()
            {
                Topic = string.Empty;
                Content = null;
            }
        }

    }
}

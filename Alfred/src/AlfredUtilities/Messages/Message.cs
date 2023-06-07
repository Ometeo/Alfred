namespace AlfredUtilities.Messages
{
    /// <summary>
    /// Message for message dispatcher.
    /// </summary>
    public class Message
    {
        #region Public Fields

        /// <summary>
        /// Null message.
        /// </summary>
        public static readonly Message Null = new NullMessage();

        #endregion Public Fields

        #region Public Properties

        /// <summary>
        /// Content of the message.
        /// </summary>
        public object? Content { get; set; }

        /// <summary>
        /// Topic of message.
        /// <para>Used for dispatch message to the correct listener.</para>
        /// </summary>
        public string Topic { get; set; } = string.Empty;

        #endregion Public Properties

        #region Private Classes

        private sealed class NullMessage : Message
        {
            #region Public Constructors

            public NullMessage() : base()
            {
                Topic = string.Empty;
                Content = null;
            }

            #endregion Public Constructors
        }

        #endregion Private Classes
    }
}

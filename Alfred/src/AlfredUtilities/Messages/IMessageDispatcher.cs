namespace AlfredUtilities.Messages
{
    /// <summary>
    /// Interface for message dispatching.
    ///
    /// <para>Plugin or other components can enqueue messages with topic on the dispatcher.</para>
    /// <para>Listeners of thoses topics receive the said messages.</para>
    /// </summary>
    public interface IMessageDispatcher
    {
        /// <summary>
        /// Enqueue message on the dispatcher.
        /// </summary>
        /// <param name="message">Message to enqueue.</param>
        void EnqueueMessage(Message message);

        /// <summary>
        /// Dequeue message on the dispatcher.
        /// </summary>
        Message DequeueMessage();

        /// <summary>
        /// Register listener to a topic.
        /// </summary>
        /// <param name="topic">topic listened.</param>
        /// <param name="listener">listener to register.</param>
        /// <returns></returns>
        bool Register(string topic, IMessageListener listener);
    }
}
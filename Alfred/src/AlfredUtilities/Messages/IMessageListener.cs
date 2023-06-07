namespace AlfredUtilities.Messages
{
    public interface IMessageListener
    {
        #region Public Methods

        public void Consume(Message message);

        #endregion Public Methods
    }
}

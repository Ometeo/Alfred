namespace AlfredUtilities.Messages
{
    public interface IMessageListener
    {
        public void Consume(Message message);
    }
}

namespace SuperBack.Communications
{
    public interface ICommunicationChannel
    {
        void Init();
        void Close();
        void Listen();
        void Send();
    }
}

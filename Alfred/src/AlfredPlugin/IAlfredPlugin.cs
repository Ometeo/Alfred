using AlfredUtilities.Messages;

namespace AlfredPlugin
{
    public interface IAlfredPlugin
    {
        string Name { get; }

        bool Register();

        void Init(IMessageDispatcher messageDispatcher);

        void Update();

    }
}

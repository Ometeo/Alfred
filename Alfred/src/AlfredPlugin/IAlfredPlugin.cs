using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

namespace AlfredPlugin
{
    public interface IAlfredPlugin
    {
        string Name { get; }

        bool Register();

        void Init(IMessageDispatcher messageDispatcher, ISensorService sensorService);

        void Update();

    }
}

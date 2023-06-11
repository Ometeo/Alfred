using AlfredPlugin;

using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

using System;
using System.Diagnostics.CodeAnalysis;

namespace FakePlugin
{
    [ExcludeFromCodeCoverage]
    internal class FakeAlfredPlugin : IAlfredPlugin, IMessageListener
    {
        #region Private Fields

        private readonly uint _sensorNumber = 10;
        private IMessageDispatcher? _messageDispatcher;
        private Sensor localSensor = Sensor.Null;

        #endregion Private Fields

        #region Public Properties

        public string Name => "Fake plugin";

        #endregion Public Properties

        #region Public Methods

        public void Consume(Message message)
        {
            if ("NewSensorResponse" == message.Topic)
            {
                Guid guid = (Guid)message.Content;
                Message readMessage = new()
                {
                    Topic = "ReadSensor",
                    Content = guid
                };

                _messageDispatcher?.EnqueueMessage(readMessage);
                _ = _messageDispatcher?.DequeueMessage();
            }
            else if ("ReadSensorResponse" == message.Topic)
            {
                localSensor = message.Content as Sensor;
            }
            else if ("UpdateSensorResponse" == message.Topic)
            {
                if ((bool)message.Content)
                {
                    Message readMessage = new()
                    {
                        Topic = "ReadSensor",
                        Content = localSensor.Id
                    };

                    _messageDispatcher?.EnqueueMessage(readMessage);
                    _ = _messageDispatcher?.DequeueMessage();
                }
            }
        }

        public void Init(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
            _ = _messageDispatcher?.Register("NewSensorResponse", this);
            _ = _messageDispatcher?.Register("UpdateSensorResponse", this);
            _ = _messageDispatcher?.Register("ReadSensorResponse", this);

            for (int i = 0; i < _sensorNumber; i++)
            {
                Sensor sensor = new($"Sensor_{i}");
                sensor.Data.Add(new SensorData("value", 0));
                Message message = new()
                {
                    Topic = "NewSensor",
                    Content = sensor
                };
                _messageDispatcher?.EnqueueMessage(message);
                _ = _messageDispatcher?.DequeueMessage();
            }
        }

        public bool Register()
        {
            return false;
        }

        public void Update()
        {
            // Empty for now.
        }

        #endregion Public Methods
    }
}

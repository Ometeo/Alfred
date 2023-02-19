using AlfredPlugin;

using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;

using System;
using System.Linq;

namespace FakePlugin
{
    class FakeAlfredPlugin : IAlfredPlugin, IMessageListener
    {
        public string Name => "Fake plugin";

        private IMessageDispatcher MessageDispatcher { get; set; }

        bool created = false;

        Sensor localSensor = Sensor.Null;

        public void Consume(Message message)
        {
            if ("NewSensorResponse" == message.Topic)
            {
                Guid guid = (Guid)message.Content;
                Message readMessage = new Message()
                {
                    Topic = "ReadSensor",
                    Content = guid
                };

                MessageDispatcher.EnqueueMessage(readMessage);
                MessageDispatcher.DequeueMessage();
            }
            else if ("ReadSensorResponse" == message.Topic)
            {
                localSensor = message.Content as Sensor;
            }
            else if ("UpdateSensorResponse" == message.Topic)
            {
                if ((bool)message.Content)
                {
                    Message readMessage = new Message()
                    {
                        Topic = "ReadSensor",
                        Content = localSensor.Id
                    };

                    MessageDispatcher.EnqueueMessage(readMessage);
                    MessageDispatcher.DequeueMessage();
                }
            }
        }

        public void Init(IMessageDispatcher messageDispatcher)
        {
            MessageDispatcher = messageDispatcher;
            MessageDispatcher.Register("NewSensorResponse", this);
            MessageDispatcher.Register("UpdateSensorResponse", this);
            MessageDispatcher.Register("ReadSensorResponse", this);

            for (int i = 0; i < 10; i++)
            {
                Sensor sensor = new Sensor($"Sensor_{i}");
                sensor.Data.Add(new SensorData("value", 0));
                Message message = new Message()
                {
                    Topic = "NewSensor",
                    Content = sensor
                };
                MessageDispatcher.EnqueueMessage(message);
                MessageDispatcher.DequeueMessage();
            }

        }

        public bool Register()
        {
            return false;
        }

        public void Update()
        {            
        }
    }
}

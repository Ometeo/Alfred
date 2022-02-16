﻿using AlfredPlugin;
using AlfredUtilities.Messages;
using AlfredUtilities.Sensors;
using System;

namespace FakePlugin
{
    class FakeAlfredPlugin : IAlfredPlugin, IMessageListener
    {
        public string Name => "Fake plugin";

        private IMessageDispatcher MessageDispatcher { get; set; }
        private ISensorService SensorService { get; set; }

        bool created = false;

        Sensor localSensor = Sensor.Null;

        public void Consume(Message message)
        {
            //if ("NewSensorResponse" == message.Topic)
            //{
            //    Guid guid = (Guid)message.Content;
            //    Message readMessage = new Message()
            //    {
            //        Topic = "ReadSensor",
            //        Content = guid
            //    };

            //    MessageDispatcher.EnqueueMessage(readMessage);
            //    MessageDispatcher.DequeueMessage();
            //}
            //else if ("ReadSensorResponse" == message.Topic)
            //{
            //    localSensor = message.Content as Sensor;
            //}
            //else if ("UpdateSensorResponse" == message.Topic)
            //{
            //    if((bool)message.Content)
            //    {
            //        Message readMessage = new Message()
            //        {
            //            Topic = "ReadSensor",
            //            Content = localSensor.Id
            //        };

            //        MessageDispatcher.EnqueueMessage(readMessage);
            //        MessageDispatcher.DequeueMessage();
            //    }
            //}
        }

        public void Init(IMessageDispatcher messageDispatcher, ISensorService sensorService)
        {
            //MessageDispatcher = messageDispatcher;         
            //MessageDispatcher.Register("NewSensorResponse", this);
            //MessageDispatcher.Register("UpdateSensorResponse", this);
            //MessageDispatcher.Register("ReadSensorResponse", this);

            SensorService = sensorService;
        }

        public bool Register()
        {
            return false;
        }

        public void Update()
        {
            Message message;

            if (!created)
            {
                Sensor sensor = new Sensor("toto");
                sensor.Data.Add(new SensorData("counter", 42));

                sensor.Id = SensorService.Add(sensor);
                created = true;
                localSensor = sensor;
            }
            else
            {
                int value = (int)localSensor.Data[0].Value;
                localSensor.Data[0].Value = ++value;

                SensorService.Update(localSensor.Id, localSensor);

            }
        }
    }
}

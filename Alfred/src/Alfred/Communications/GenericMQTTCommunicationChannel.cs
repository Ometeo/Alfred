using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperBack.Communications
{
    class GenericMQTTCommunicationChannel : ICommunicationChannel
    {
        IMqttClient client;

        delegate void TopicCallback(string topic, string message);

        Dictionary<string, TopicCallback> callbacks = new Dictionary<string, TopicCallback>();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            var factory = new MqttFactory();            
            client = factory.CreateMqttClient();

            callbacks.Add("sensor_data", (t, m) =>
            {
                Console.WriteLine($"Sensor data received : {m}");
                Message.Message message = new Message.Message()
                {
                    Topic = t,
                    Content = m
                };
            });

            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("localhost", 2552)
                .Build();

            client.UseConnectedHandler(async e =>
            {
                foreach(var kvp in callbacks)
                {
                    await client.SubscribeAsync(new TopicFilterBuilder().WithTopic(kvp.Key).Build());
                }                
            });

            client.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("###DISCONNECTED FROM SERVER###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await client.ConnectAsync(options, CancellationToken.None);
                }
                catch (Exception)
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });

            client.ConnectAsync(options, CancellationToken.None);            

            client.UseApplicationMessageReceivedHandler(e =>
            {                
                callbacks[e.ApplicationMessage.Topic](e.ApplicationMessage.Topic, System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
            });
        }

        public void Listen()
        {
            throw new NotImplementedException();
        }

        public void Send()
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("update_sensor")
                .WithPayload("...")
                .Build();

            client.PublishAsync(message);
        }
    }
}

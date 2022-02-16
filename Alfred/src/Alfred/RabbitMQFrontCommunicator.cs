using AlfredFrontInterface;

using AlfredUtilities.Sensors;

using Newtonsoft.Json;

using RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfred
{
    public class RabbitMQFrontCommunicator : IFrontCommunicator<Sensor>
    {      
        public void Send(Sensor message)
        {
            if(message != Sensor.Null)
            {               
                var factory = new ConnectionFactory() { HostName = "localhost"};
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "SensorQueue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string text = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(text);


                    channel.BasicPublish(exchange: "",
                                         routingKey: "SensorQueue",
                                         basicProperties: null,
                                         body: body);
                }
            }           
        }
    }
}

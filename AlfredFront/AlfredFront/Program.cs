using AlfredFront.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<SensorService>();

var app = builder.Build();

var sensorService = app.Services.GetRequiredService<SensorService>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "SensorQueue",
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var sensor = JsonConvert.DeserializeObject<Sensor>(message);
        
        if(sensor != null)
        {
            sensorService.Update(sensor);
        }        
        

        Console.WriteLine(" Received {0}", message);
    };
    channel.BasicConsume(queue: "SensorQueue",
                         autoAck: true,
                         consumer: consumer);

    app.Run();
}



using Alfred.Messages;
using Alfred.Plugins;
using Alfred.SensorsService;

using AlfredUtilities.Messages;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace Alfred
{
    internal static class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IPluginPathFinder, PluginPathFinder>()
                .AddSingleton<IPluginStore, PluginStore>()
                .AddSingleton<ISensorsService, SensorsService.SensorsService>()
                .AddSingleton<IMessageDispatcher, MessageDispatcher>()
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<MainApp>()
                .AddLogging(loggingBuilder =>
                {
                    // configure Logging with NLog
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog();
                })
                .AddHostedService<MainApp>();

            using var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        #endregion Private Methods
    }
}

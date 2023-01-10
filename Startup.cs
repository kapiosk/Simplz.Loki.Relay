using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Serilog.Sinks.Grafana.Loki;
using Serilog;
using Simplz.Loki.Relay.Options;
using System.Diagnostics;

[assembly: FunctionsStartup(typeof(Simplz.Loki.Relay.Startup))]
namespace Simplz.Loki.Relay
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var config = builder.ConfigurationBuilder.Build();
            var dotenv = Path.Combine(Directory.GetCurrentDirectory(), "local.settings.json");
            builder.ConfigurationBuilder.AddJsonFile(dotenv, true);
            var appConfig = config.GetConnectionString("AppConfig");
            if (!string.IsNullOrEmpty(appConfig))
                builder.ConfigurationBuilder.AddAzureAppConfiguration(appConfig);
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            var grafanaLokiOptions = configuration.GetRequiredSection("GrafanaLokiOptions").Get<GrafanaLokiOptions>();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Fatal()
                            .MinimumLevel.Override("Function", Serilog.Events.LogEventLevel.Error)
                            //.MinimumLevel.Override("Simplz", Serilog.Events.LogEventLevel.Information)
                            .WriteTo.GrafanaLoki(
                                    grafanaLokiOptions.Uri,
                                    new[] { new LokiLabel { Key = grafanaLokiOptions.LokiLabelsKey, Value = grafanaLokiOptions.LokiLabelsValue } },
                                    credentials: grafanaLokiOptions.LokiCredentials,
                                    period: TimeSpan.FromMilliseconds(50)
                                )
                            .CreateLogger();

            builder.Services.AddLogging((loggingBuilder) =>
            {
                loggingBuilder.AddSerilog(Log.Logger, dispose: true);
            });
        }
    }
}
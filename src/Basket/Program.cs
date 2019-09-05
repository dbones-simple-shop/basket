using Core.Infrastructure;
using Core.Infrastructure.Application;
using Core.Infrastructure.Logging;
using Core.Infrastructure.Marten;
 using Core.Infrastructure.HealthChecks;
using Core.Infrastructure.MassTransit;
using Core.Infrastructure.Redis;
using Core.Infrastructure.Serializing;
using Core.Infrastructure.Swagger;
using Core.Infrastructure.Tracing;
using Core.Infrastructure.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Basket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile(Path.Combine("config", "stagesettings.json"))
                //.AddEnvironmentVariables()
                //.AddCommandLine(args)
                .Build();

            var port = configuration.GetSection("Application").Get<ApplicationConfiguration>().PortNumber;

            var host = WebHost.CreateDefaultBuilder(args)
                
                .UseConfiguration(configuration)
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                })
                .ConfigureLogging()
                //.ConfigureMartin()
                .ConfigureSwagger()
                .ConfigureHeathChecks()
                .ConfigureSerializer()
                .ConfigureRedis()
                .ConfigureMassTransit()
                .ConfigureTracing(configuration)
                //.ConfigureWebApi()
                .UseUrls($"http://*:{port}")
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

      
    }
}
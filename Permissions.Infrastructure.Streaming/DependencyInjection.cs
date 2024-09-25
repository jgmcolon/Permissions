using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Permissions.Domain.Streaming;
using Permissions.Infrastructure.Streaming.Services;
using Permissions.Shared;
using System.Net;
using System.Text;
using static Confluent.Kafka.ConfigPropertyNames;
namespace Permissions.Infrastructure.Streaming
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddKafka(this IServiceCollection services, Appsettings appsetting)
        {

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = appsetting?.Streaming?.BootstrapServers
            };

            Console.WriteLine($"ProducerConfig-Server: {appsetting?.Streaming?.BootstrapServers}");

            services.AddSingleton<ProducerConfig>(producerConfig);

            services.AddScoped<IPermissionStreaming, PermissionStreaming>();

            return services;
        }
    }
}

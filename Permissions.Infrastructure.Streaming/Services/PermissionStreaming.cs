using Confluent.Kafka;
using Permissions.Domain.Streaming;
using Permissions.Domain.IndexDto.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Permissions.Infrastructure.Streaming.Services
{
    public class PermissionStreaming : IPermissionStreaming
    {
        private readonly string topic = "permission";

        private readonly IProducer<string, string> _producer;

        public PermissionStreaming(ProducerConfig producerconfig)
        {
            _producer = new ProducerBuilder<string, string>(producerconfig).Build();
        }

        public async Task ProduceAsync(PermissionDto message)
        {
            await _producer.ProduceAsync(topic, new Message<string, string> { Key = message.Id, Value = JsonSerializer.Serialize(message), });
        }

    }
}

using Confluent.Kafka;
using CQRS.Core.Application.Consumers;
using CQRS.Core.Application.Events;
using Microsoft.Extensions.Options;
using Post.Query.Application.Handlers;
using Post.Query.Infrastructure.Utils;
using System.Text.Json;

namespace Post.Query.Infrastructure.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IEventHandler _eventHandler;

        public EventConsumer(IOptions<ConsumerConfig> config, IEventHandler eventHandler)
        {
            _consumerConfig = config.Value;
            _eventHandler = eventHandler;
        }

        public void Consume(string topic)
        {
            using (var consumer = new ConsumerBuilder<string, string>(_consumerConfig)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build()) 
            {
                consumer.Subscribe(topic);

                while (true) 
                {
                    var consumerResult = consumer.Consume();

                    if (consumerResult?.Message == null) continue;

                    var options = new JsonSerializerOptions 
                    { 
                        Converters = { new JsonEventConverter() } 
                    };
                    var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, options);
                    var handleMethod = @event?.GetType()?.GetMethod("On", new Type[] { @event.GetType() });

                    if (handleMethod == null)
                    { 
                        throw new ArgumentNullException(nameof(handleMethod), "Could not find event handler method!");
                    }

                    handleMethod.Invoke(_eventHandler, new object[] { @event! });
                    consumer.Commit(consumerResult);
                }
            }
        }
    }
}

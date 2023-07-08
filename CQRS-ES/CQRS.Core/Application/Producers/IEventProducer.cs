using CQRS.Core.Application.Events;

namespace CQRS.Core.Application.Producers
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent;
    }
}

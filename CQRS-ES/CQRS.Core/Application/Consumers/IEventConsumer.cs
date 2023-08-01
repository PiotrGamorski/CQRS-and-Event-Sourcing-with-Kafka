namespace CQRS.Core.Application.Consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}

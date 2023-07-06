using CQRS.Core.Domain.Primitives;

namespace CQRS.Core.Application.Persistance
{
    public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(AggregateRoot aggregate);
        Task<T> GetByIdAsync(Guid aggregateId);
    }
}

using CQRS.Core.Application.Events;

namespace CQRS.Core.Application.Persistance.Repositories
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<List<EventModel>> FindByAggreagateIdAsync(Guid aggregateId);
    }
}

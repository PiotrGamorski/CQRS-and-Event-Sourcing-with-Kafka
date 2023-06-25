using CQRS.Core.Events;

namespace CQRS.Core.Application.Persistance
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<List<EventModel>> FindByAggreagateIdAsync(Guid aggregateId);
    }
}

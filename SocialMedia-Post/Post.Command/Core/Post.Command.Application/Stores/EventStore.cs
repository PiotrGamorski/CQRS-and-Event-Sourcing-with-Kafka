using CQRS.Core.Application.Events;
using CQRS.Core.Application.Persistance;
using CQRS.Core.Application.Persistance.Repositories;
using CQRS.Core.Domain.Exceptions;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Stores
{
    public class EventStore : IEventStore
    {
        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        private readonly IEventStoreRepository _eventStoreRepository;

        public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggreagateIdAsync(aggregateId);

            if (eventStream == null || !eventStream.Any())
            {
                throw new AggregateNotFoundException("Incorrect post ID provided");
            }

            var result = eventStream
                .OrderBy(eventModel => eventModel.Version)
                .Select(eventModel => eventModel.EventData)
                .ToList();

            return result;
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggreagateIdAsync(aggregateId);

            if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            {
                throw new ConcurrencyException();
            }

            var version = expectedVersion;
            foreach (var @event in events)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;
                var eventModel = new EventModel()
                {
                    TimeStamp = DateTime.UtcNow,
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(PostAggregate),
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };

                await _eventStoreRepository.SaveAsync(eventModel);
            }
        }
    }
}

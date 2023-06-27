using CQRS.Core.Application.Handlers;
using CQRS.Core.Domain.Primitives;
using CQRS.Core.Infrastructure;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
    {
        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        private readonly IEventStore _eventStore;

        public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new PostAggregate();
            var events = await _eventStore.GetEventsAsync(aggregateId);

            if (events == null || !events.Any())
            {
                return aggregate;
            }

            aggregate.ReplayEvents(events);
            var latestVersion = events
                .Select(e => e.Version)
                .Max();

            aggregate.Version = latestVersion;

            return aggregate;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommitedChanges(), aggregate.Version);
            aggregate.MarkChangesAsCommited();
        }
    }
}

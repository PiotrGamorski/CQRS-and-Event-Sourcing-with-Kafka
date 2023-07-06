using CQRS.Core.Application.Events;

namespace CQRS.Core.Domain.Primitives
{
    public abstract class AggregateRoot
    {
        private readonly List<BaseEvent> _changes = new();
        protected Guid _id;

        public Guid Id { get { return _id; } }
        public int Version { get; set; } = -1;

        public IEnumerable<BaseEvent> GetUncommitedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommited()
        { 
            _changes.Clear();
        }

        protected void RaiseEvent(BaseEvent @event)
        { 
            ApplyChanges(@event, true);
        }

        private void ApplyChanges(BaseEvent @event, bool isNew)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}.");
            }

            method.Invoke(this, new object[] { @event });

            if (isNew)
            {
                _changes.Add(@event);
            }
        }

        public void ReplayEvents(IEnumerable<BaseEvent> events)
        {
            foreach (var @event in events)
            { 
                ApplyChanges(@event, false);
            }
        }
    }
}

using CQRS.Core.Events;

namespace Post.Common.Events.MessageUpdatedEvent
{
    public sealed class MessageUpdatedEvent : BaseEvent
    {
        public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
        {
        }

        public string Message { get; init; } = null!;
    }
}

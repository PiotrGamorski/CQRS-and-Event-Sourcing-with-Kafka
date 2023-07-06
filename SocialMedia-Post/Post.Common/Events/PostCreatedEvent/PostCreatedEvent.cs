using CQRS.Core.Application.Events;

namespace Post.Common.Events.PostCreatedEvent
{
    public sealed class PostCreatedEvent : BaseEvent
    {
        public PostCreatedEvent() : base(nameof(PostCreatedEvent))
        {
        }

        public string Author { get; init; } = null!;
        public string Message { get; init; } = null!;
        public DateTime DatePosted { get; init; }
    }
}

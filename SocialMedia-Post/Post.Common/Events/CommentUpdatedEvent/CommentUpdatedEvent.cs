using CQRS.Core.Events;

namespace Post.Common.Events.CommentUpdatedEvent
{
    public sealed class CommentUpdatedEvent : BaseEvent
    {
        public CommentUpdatedEvent() : base(nameof(CommentUpdatedEvent))
        {
        }

        public Guid CommentId { get; init; }
        public string Comment { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public DateTime EditedDate { get; init; }
    }
}

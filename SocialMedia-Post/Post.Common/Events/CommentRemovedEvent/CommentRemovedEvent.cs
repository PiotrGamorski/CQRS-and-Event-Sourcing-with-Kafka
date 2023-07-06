using CQRS.Core.Application.Events;

namespace Post.Common.Events.CommentRemovedEvent
{
    public sealed class CommentRemovedEvent : BaseEvent
    {
        public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
        {
        }

        public Guid CommentId { get; init; }
    }
}

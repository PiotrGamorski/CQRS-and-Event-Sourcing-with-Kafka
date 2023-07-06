using CQRS.Core.Application.Events;

namespace Post.Common.Events.CommentAddedEvent
{
    public sealed class CommentAddedEvent : BaseEvent
    {
        public CommentAddedEvent() : base(nameof(CommentAddedEvent))
        {
        }

        public Guid CommentId { get; init; }
        public string Comment { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public DateTime CommntAddedDate { get; init; }
    }
}

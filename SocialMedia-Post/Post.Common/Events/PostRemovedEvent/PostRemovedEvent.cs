using CQRS.Core.Application.Events;

namespace Post.Common.Events.PostEventRemoved
{
    public sealed class PostRemovedEvent : BaseEvent
    {
        public PostRemovedEvent() : base(nameof(PostRemovedEvent))
        {
        }

        public Guid CommentId { get; set; }
    }
}

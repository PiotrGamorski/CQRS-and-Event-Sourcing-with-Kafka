using CQRS.Core.Application.Events;

namespace Post.Common.Events.PostLikedEvent
{
    public sealed class PostLikedEvent : BaseEvent
    {
        public PostLikedEvent() : base(nameof(PostLikedEvent))
        {
        }
    }
}

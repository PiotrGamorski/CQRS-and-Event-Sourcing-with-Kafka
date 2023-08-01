using Post.Common.Events.CommentAddedEvent;
using Post.Common.Events.CommentRemovedEvent;
using Post.Common.Events.CommentUpdatedEvent;
using Post.Common.Events.MessageUpdatedEvent;
using Post.Common.Events.PostCreatedEvent;
using Post.Common.Events.PostEventRemoved;
using Post.Common.Events.PostLikedEvent;

namespace Post.Query.Application.Handlers
{
    public interface IEventHandler
    {
        Task On(PostCreatedEvent @event);
        Task On(MessageUpdatedEvent @event);
        Task On(PostLikedEvent @event);
        Task On(CommentAddedEvent @event);
        Task On(CommentUpdatedEvent @event);
        Task On(CommentRemovedEvent @event);
        Task On(PostRemovedEvent @event);
    }
}

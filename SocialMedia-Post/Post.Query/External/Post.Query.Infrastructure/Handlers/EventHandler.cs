using Post.Common.Events.CommentAddedEvent;
using Post.Common.Events.CommentRemovedEvent;
using Post.Common.Events.CommentUpdatedEvent;
using Post.Common.Events.MessageUpdatedEvent;
using Post.Common.Events.PostCreatedEvent;
using Post.Common.Events.PostEventRemoved;
using Post.Common.Events.PostLikedEvent;
using Post.Query.Application.Handlers;
using Post.Query.Application.Persistance.Repositories;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastructure.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public EventHandler(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;

        }

        public async Task On(PostCreatedEvent @event)
        {
            var post = new PostEntity
            {
                PostId = @event.Id,
                Author = @event.Author,
                DatePosted = @event.DatePosted,
                Message = @event.Message,
            };

            await _postRepository.CreateAsync(post);
        }

        public async Task On(MessageUpdatedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null)
            {
                return;
            }

            post.Message = @event.Message;
            await _postRepository.UpdateAsync(post);    
        }

        public async Task On(PostLikedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if (post == null)
            {
                return;
            }

            post.Likes++;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(CommentAddedEvent @event)
        {
            var comment = new CommentEntity
            {
                PostId = @event.Id,
                CommentId = @event.CommentId,
                CommentDate = @event.CommntAddedDate,
                Comment = @event.Comment,
                Username = @event.UserName,
                Edited = false,
            };

            await _commentRepository.CreateAsync(comment);
        }

        public async Task On(CommentUpdatedEvent @event)
        {
            var comment = await _commentRepository.GetByIdAsync(@event.CommentId);

            if (comment == null)
            {
                return;
            }

            comment.Comment = @event.Comment;
            comment.Edited = true;
            comment.CommentDate = @event.EditedDate;

            await _commentRepository.UpdateAsync(comment);
        }

        public async Task On(CommentRemovedEvent @event)
        {
            await _commentRepository.DeleteAsync(@event.CommentId);
        }

        public async Task On(PostRemovedEvent @event)
        {
            await _postRepository.DeleteAsync(@event.Id);
        }
    }
}

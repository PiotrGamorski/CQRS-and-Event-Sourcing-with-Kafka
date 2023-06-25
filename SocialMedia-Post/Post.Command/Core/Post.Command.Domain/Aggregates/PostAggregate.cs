using CQRS.Core.Domain;
using Post.Common.Events.CommentAddedEvent;
using Post.Common.Events.CommentRemovedEvent;
using Post.Common.Events.CommentUpdatedEvent;
using Post.Common.Events.MessageUpdatedEvent;
using Post.Common.Events.PostCreatedEvent;
using Post.Common.Events.PostEventRemoved;
using Post.Common.Events.PostLikedEvent;

namespace Post.Command.Domain.Aggregates
{
    public class PostAggregate : AggregateRoot
    {
        public PostAggregate() 
        {
        }

        public PostAggregate(Guid id, string author, string message)
        {
            RaiseEvent(new PostCreatedEvent()
            {
                Id = id,
                Author = author,
                Message = message,
                DatePosted = DateTime.UtcNow
            });
        }

        public void Apply(PostCreatedEvent @event)
        {
            _id = @event.Id;
            _active = true;
            _author = @event.Author;
        }

        private bool _active;
        private string _author = null!;
        private Dictionary<Guid, Tuple<string, string>> _comments = new();

        public bool Active 
        {
            get => _active; set => _active = value;
        }

        #region EditMessage
        public void EditMessage(string message) 
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit the message of an inactive post.");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException($"The value of {nameof(message)} cannot be null or empty. Please provide a valid {nameof(message)}.");
            }

            RaiseEvent(new MessageUpdatedEvent()
            {
                Id = _id,
                Message = message,
            });
        }

        public void Apply(MessageUpdatedEvent @event)
        {
            _id = @event.Id;
        }
        #endregion

        #region LikePost
        public void LikePost()
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot like an inactive post.");
            }

            RaiseEvent(new PostLikedEvent()
            { 
                Id = _id,
            });
        }

        public void Apply(PostLikedEvent @event)
        {
            _id = @event.Id;
        }
        #endregion

        #region AddComment
        public void AddComment(string comment, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot add a comment to an inactive post.");
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new InvalidOperationException($"The value of {nameof(comment)} cannot be null or empty. Please provide a valid {nameof(comment)}.");
            }
             
            RaiseEvent(new CommentAddedEvent()
            { 
                Id = _id,
                CommentId = Guid.NewGuid(),
                Comment = comment,
                UserName = username,
                CommntAddedDate = DateTime.UtcNow,
            });
        }

        public void Apply(CommentAddedEvent @event)
        {
            _id = @event.Id;
            _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.UserName));
        }
        #endregion

        #region EditComment
        public void EditComment(Guid commentId, string comment, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit a comment of an inactive post.");
            }

            if (_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You are not allowed to edit a comment that was made by another user.");
            }

            RaiseEvent(new CommentUpdatedEvent()
            {
                Id = _id,
                CommentId = commentId,
                Comment = comment,
                UserName = username,
                EditedDate = DateTime.UtcNow,
            });
        }

        public void Apply(CommentUpdatedEvent @event)
        {
            _id = @event.Id;
            _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.UserName);
        }
        #endregion

        #region RemoveComment
        public void RemoveComment(Guid commentId, string username) 
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot remove a comment of an inactive post.");
            }

            if (!_comments.ContainsKey(commentId))
            {
                throw new InvalidOperationException("Invalid input data. Comment to be removed not found.");
            }

            if (_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You are not allowed to remove a comment that was made by another user.");
            }

            RaiseEvent(new CommentRemovedEvent()
            {
                Id = _id,
                CommentId = commentId,
            });
        }

        public void Apply(CommentRemovedEvent @event)
        {
            _id = @event.Id;
            _comments.Remove(@event.CommentId);
        }
        #endregion

        #region DeletePost
        public void DeletePost(string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("The post has already been removed");
            }

            if (!_author.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You are not allowed to delete a post that was made by someone else.");
            }

            RaiseEvent(new PostRemovedEvent()
            { 
                Id = _id,
            });
        }

        public void Apply(PostRemovedEvent @event)
        {
            _id = @event.Id;
            _active = false;
        }
        #endregion
    }
}

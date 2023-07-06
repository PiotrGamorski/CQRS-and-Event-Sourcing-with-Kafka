using CQRS.Core.Application.Persistance;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Commands.RemoveCommentCommand
{
    public interface IRemoveCommentCommandHandler
    {
        Task HandleAsync(RemoveCommentCommand command);
    }

    public class RemoveCommentCommandHandler : IRemoveCommentCommandHandler
    {
        public RemoveCommentCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public async Task HandleAsync(RemoveCommentCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.RemoveComment(command.CommentId, command.UserName);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}

using CQRS.Core.Application.Persistance;
using Post.Command.Application.Handlers;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Commands.EditCommentCommand
{
    public interface IEditCommentCommandHandler
    { 
        Task HandleAsync(EditCommentCommand command);
    }

    public class EditCommentCommandHandler : IEditCommentCommandHandler
    {
        public EditCommentCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public async Task HandleAsync(EditCommentCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditComment(command.Id, command.Comment, command.UserName);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}

using CQRS.Core.Application.Persistance;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Commands.AddCommentCommand
{
    public interface IAddCommentCommandHandler
    {
        Task HandleAsync(AddCommentCommand command);
    }

    public class AddCommentCommandHandler : IAddCommentCommandHandler
    {
        public AddCommentCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public async Task HandleAsync(AddCommentCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.AddComment(command.Comment, command.UserName);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}

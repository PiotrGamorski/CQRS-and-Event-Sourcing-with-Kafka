using CQRS.Core.Application.Persistance;
using CQRS.Core.Domain.Primitives;
using Post.Command.Application.Handlers;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Commands.EditMessageCommand
{
    public interface IEditMessageCommandHandler
    {
        Task HandleAsync(EditMessageCommand command);
    }

    public class EditMessageCommandHandler : IEditMessageCommandHandler
    {
        public EditMessageCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public async Task HandleAsync(EditMessageCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditMessage(command.Message);

            await _eventSourcingHandler.SaveAsync(aggregate); 
        }
    }
}

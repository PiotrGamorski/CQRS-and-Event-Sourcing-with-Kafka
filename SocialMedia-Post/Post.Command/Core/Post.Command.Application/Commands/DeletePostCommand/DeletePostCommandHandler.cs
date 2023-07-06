using CQRS.Core.Application.Persistance;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Commands.DeletePostCommand
{
    public interface IDeletePostCommandHandler
    { 
        Task HandleAsync(DeletePostCommand command);
    }

    public class DeletePostCommandHandler : IDeletePostCommandHandler
    {
        public DeletePostCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public async Task HandleAsync(DeletePostCommand command)
        {
            var aggergate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggergate.DeletePost(command.UserName);

            await _eventSourcingHandler.SaveAsync(aggergate);
        }
    }
}

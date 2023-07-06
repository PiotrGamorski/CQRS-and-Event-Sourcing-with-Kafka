using CQRS.Core.Application.Persistance;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Commands.NewPostCommand
{
    public interface INewPostCommandHandler
    {
        Task HandleAsync(NewPostCommand command);
    }

    public class NewPostCommandHandler : INewPostCommandHandler
    {
        public NewPostCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;    
        }

        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public async Task HandleAsync(NewPostCommand command)
        {
            var aggregate = new PostAggregate(command.Id, command.Author, command.Message);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}

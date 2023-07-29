using CQRS.Core.Application.Persistance;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application.Commands.LikePostCommand
{
    public interface ILikePostCommandHandler
    {
        Task HandleAsync(LikePostCommand command);
    }

    public class LikePostCommandHandler : ILikePostCommandHandler
    {
        public LikePostCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;   
        }

        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public async Task HandleAsync(LikePostCommand command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.LikePost();

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}

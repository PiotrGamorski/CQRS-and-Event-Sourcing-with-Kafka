using CQRS.Core.Application.Commands;
using CQRS.Core.Infrastructure;

namespace Post.Command.Infrastructure.Dispatchers
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new(); 

        public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                throw new IndexOutOfRangeException("You cannot register the same command handler twice.");
            }

            _handlers.Add(typeof(T), baseCommand => handler((T) baseCommand));
        }

        public async Task SendAsync(BaseCommand command)
        {
            if (_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task>? handler))
            {
                await handler(command);
            }
            else
            { 
                throw new ArgumentNullException(nameof(handler), "No command handler was registered." );
            }
        }
    }
}

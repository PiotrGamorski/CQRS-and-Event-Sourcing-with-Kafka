using CQRS.Core.Application.Commands;

namespace Post.Command.Application.Commands.DeletePostCommand
{
    public sealed class DeletePostCommand : BaseCommand
    {
        public string UserName { get; init; } = null!;
    }
}

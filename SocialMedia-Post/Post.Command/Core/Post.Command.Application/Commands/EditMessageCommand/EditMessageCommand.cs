using CQRS.Core.Commands;

namespace Post.Command.Application.Commands.EditMessageCommand
{
    public sealed class EditMessageCommand : BaseCommand
    {
        public string Message { get; init; } = null!;
    }
}

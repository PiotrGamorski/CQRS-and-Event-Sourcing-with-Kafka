using CQRS.Core.Application.Commands;

namespace Post.Command.Application.Commands.AddCommentCommand
{
    public sealed class AddCommentCommand : BaseCommand
    {
        public string Comment { get; init; } = null!;
        public string UserName { get; init; } = null!;
    }
}

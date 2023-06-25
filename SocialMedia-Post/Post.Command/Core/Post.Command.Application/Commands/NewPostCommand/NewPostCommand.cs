using CQRS.Core.Application.Commands;

namespace Post.Command.Application.Commands.NewPostCommand
{
    public sealed class NewPostCommand : BaseCommand
    {
        public string Author { get; init; } = null!;
        public string Message { get; init; } = null!;
    }
}

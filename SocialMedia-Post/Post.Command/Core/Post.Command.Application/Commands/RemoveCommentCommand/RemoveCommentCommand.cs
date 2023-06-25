using CQRS.Core.Commands;

namespace Post.Command.Application.Commands.RemoveCommentCommand
{
    public sealed class RemoveCommentCommand : BaseCommand
    {
        public Guid CommentId { get; init; }
        public string UserName { get; init; } = null!;
    }
}

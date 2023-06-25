using CQRS.Core.Commands;

namespace Post.Command.Application.Commands.EditCommentCommand
{
    public sealed class EditCommentCommand : BaseCommand
    {
        public Guid CommentId { get; init; }
        public string Comment { get; init; } = null!;
        public string UserName { get; init; } = null!;
    }
}

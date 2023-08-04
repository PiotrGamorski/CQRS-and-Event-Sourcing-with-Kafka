using Post.Common.DTOs;

namespace Post.Command.Application.DTOs
{
    public class NewPostResponse : BaseResponse
    {
        public Guid Id { get; init; }
    }
}

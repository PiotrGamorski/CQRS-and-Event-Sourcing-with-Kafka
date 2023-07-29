using Post.Query.Domain.Entities;

namespace Post.Query.Application.Persistance.Repositories
{
    public interface ICommentRepository
    {
        Task CreateAsync(CommentEntity commentEntity);
        Task UpdateAsync(CommentEntity commentEntity);
        Task DeleteAsync(Guid commentId);
        Task<CommentEntity> GetByIdAsync(Guid commentId);
    }
}

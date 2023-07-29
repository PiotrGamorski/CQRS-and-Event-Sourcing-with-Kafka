using Post.Query.Application.Persistance.Repositories;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastructure.Persistance.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public Task CreateAsync(CommentEntity commentEntity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<CommentEntity> GetByIdAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CommentEntity commentEntity)
        {
            throw new NotImplementedException();
        }
    }
}

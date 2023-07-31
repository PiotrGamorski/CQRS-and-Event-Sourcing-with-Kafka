using Post.Query.Domain.Entities;

namespace Post.Query.Application.Persistance.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(PostEntity postEntity);
        Task UpdateAsync(PostEntity postEntity);
        Task DeleteAsync(Guid postId);
        Task<PostEntity?> GetByIdAsync(Guid postId);
        Task<List<PostEntity>> ListAllAsync();
        Task<List<PostEntity>> ListByAuthorAsync(string author);
        Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes);
        Task<List<PostEntity>> ListWithCommentsAsync();
    }
}

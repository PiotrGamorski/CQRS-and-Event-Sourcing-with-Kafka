﻿using Microsoft.EntityFrameworkCore;
using Post.Query.Application.Persistance.Repositories;
using Post.Query.Domain.Entities;
using Post.Query.Infrastructure.Persistance.DataAccess;

namespace Post.Query.Infrastructure.Persistance.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public PostRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(PostEntity postEntity)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            { 
                context.Posts.Add(postEntity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid postId)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                var post = await GetByIdAsync(postId);

                if (post == null)
                {
                    return;
                }

                context.Posts.Remove(post);
                await context.SaveChangesAsync();
            }
        }

        public async Task<PostEntity?> GetByIdAsync(Guid postId)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                return await context.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.PostId == postId);
            }
        }

        public async Task<List<PostEntity>> ListAllAsync()
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext()) 
            {
                return await context.Posts
                    .AsNoTracking()
                    .Include(p => p.Comments)
                    .ToListAsync();
            }
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                return await context.Posts
                    .Include(p => p.Comments)
                    .Where(p => p.Author.Contains(author))
                    .ToListAsync();
            }
        }

        public async Task<List<PostEntity>> ListWithCommentsAsync()
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                return await context.Posts
                    .AsNoTracking()
                    .Include(p => p.Comments)
                    .Where(p => p.Comments != null && p.Comments.Any())
                    .ToListAsync();
            }
        }

        public async Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                return await context.Posts
                    .AsNoTracking()
                    .Include(p => p.Comments)
                    .Where(p => p.Likes >= numberOfLikes)
                    .ToListAsync();
            }
        }

        public async Task UpdateAsync(PostEntity postEntity)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                context.Posts.Update(postEntity);
                _ = await context.SaveChangesAsync();
            }
        }
    }
}

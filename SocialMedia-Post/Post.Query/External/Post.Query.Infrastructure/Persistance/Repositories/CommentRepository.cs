﻿using Microsoft.EntityFrameworkCore;
using Post.Query.Application.Persistance.Repositories;
using Post.Query.Domain.Entities;
using Post.Query.Infrastructure.Persistance.DataAccess;
using System.ComponentModel.Design;

namespace Post.Query.Infrastructure.Persistance.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public CommentRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(CommentEntity commentEntity)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                await context.Comments.AddAsync(commentEntity);
                _ = await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid commentId)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                var comment = await GetByIdAsync(commentId);

                if (comment == null)
                {
                    return;
                }

                context.Comments.Remove(comment);
                _ = await context.SaveChangesAsync();
            }
        }

        public async Task<CommentEntity?> GetByIdAsync(Guid commentId)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                return await context.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);
            }
        }

        public async Task UpdateAsync(CommentEntity commentEntity)
        {
            using (DatabaseContext context = _contextFactory.CreateDbContext())
            {
                context.Comments.Update(commentEntity);
                _ = await context.SaveChangesAsync();
            }
        }
    }
}

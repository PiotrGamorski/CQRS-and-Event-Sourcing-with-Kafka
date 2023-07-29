﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Query.Application.Persistance.Repositories;
using Post.Query.Infrastructure.Persistance.DataAccess;
using Post.Query.Infrastructure.Persistance.Repositories;

namespace Post.Query.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddPersistance(configuration);

            return services;
        }

        private static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDataAccess(configuration);
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            return services;
        }

        private static IServiceCollection AddDataAccess(this IServiceCollection services, ConfigurationManager configuration) 
        {
            Action<DbContextOptionsBuilder> configureDbContext = optionsBuilder =>
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));
            };

            services.AddDbContext<DatabaseContext>(configureDbContext);
            services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

            return services;
        }
    }
}

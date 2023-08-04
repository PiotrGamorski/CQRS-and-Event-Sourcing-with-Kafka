using CQRS.Core.Application.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Post.Command.Application.Commands.AddCommentCommand;
using Post.Command.Application.Commands.DeletePostCommand;
using Post.Command.Application.Commands.EditCommentCommand;
using Post.Command.Application.Commands.EditMessageCommand;
using Post.Command.Application.Commands.LikePostCommand;
using Post.Command.Application.Commands.NewPostCommand;
using Post.Command.Application.Commands.RemoveCommentCommand;
using Post.Command.Application.Handlers;
using Post.Command.Application.Stores;
using Post.Command.Domain.Aggregates;

namespace Post.Command.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommandHandlers();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services) 
        {
            services.AddScoped<IAddCommentCommandHandler, AddCommentCommandHandler>();
            services.AddScoped<IDeletePostCommandHandler, DeletePostCommandHandler>();
            services.AddScoped<IEditCommentCommandHandler, EditCommentCommandHandler>();
            services.AddScoped<IEditMessageCommandHandler, EditMessageCommandHandler>();
            services.AddScoped<ILikePostCommandHandler, LikePostCommandHandler>();
            services.AddScoped<INewPostCommandHandler, NewPostCommandHandler>();
            services.AddScoped<IRemoveCommentCommandHandler, RemoveCommentCommandHandler>();
            

            return services;
        }
    }
}

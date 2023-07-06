using Microsoft.Extensions.DependencyInjection;
using Post.Command.Application.Commands.EditCommentCommand;
using Post.Command.Application.Commands.EditMessageCommand;
using Post.Command.Application.Commands.LikePostCommand;
using Post.Command.Application.Commands.NewPostCommand;

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
            services.AddScoped<INewPostCommandHandler, NewPostCommandHandler>();
            services.AddScoped<IEditMessageCommandHandler, EditMessageCommandHandler>();
            services.AddScoped<IEditCommentCommandHandler, EditCommentCommandHandler>();
            services.AddScoped<ILikePostCommandHandler, LikePostCommandHandler>();

            return services;
        }
    }
}

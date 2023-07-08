using CQRS.Core;
using CQRS.Core.Application.Persistance;
using CQRS.Core.Application.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Post.Command.Application.Commands.AddCommentCommand;
using Post.Command.Application.Commands.DeletePostCommand;
using Post.Command.Application.Commands.EditCommentCommand;
using Post.Command.Application.Commands.EditMessageCommand;
using Post.Command.Application.Commands.LikePostCommand;
using Post.Command.Application.Commands.NewPostCommand;
using Post.Command.Application.Commands.RemoveCommentCommand;
using Post.Command.Application.Stores;
using Post.Command.Infrastructure.Config;
using Post.Command.Infrastructure.Dispatchers;
using Post.Command.Infrastructure.Repositories;

namespace Post.Command.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            ConfigurationManager configuration, 
            IServiceProvider serviceProvider)
        {
            services.RegisterCommandHandlers(serviceProvider);
            services.AddMongoDbConfig(configuration);
            services.AddProducerConfig(configuration);
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, EventStore>();

            return services;
        }

        private static IServiceCollection AddMongoDbConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            var mongoDbConfig = new MongoDbConfig();
            configuration.Bind(MongoDbConfig.SectionName, mongoDbConfig);
            services.AddSingleton(Options.Create(mongoDbConfig));

            return services;
        }

        private static IServiceCollection AddProducerConfig(this IServiceCollection services, ConfigurationManager configuration)
        { 
            var producerConfig = new ProducerConfig();
            configuration.Bind(producerConfig);
            services.AddSingleton(Options.Create(producerConfig));

            return services;
        }

        private static IServiceCollection RegisterCommandHandlers(this IServiceCollection services, IServiceProvider serviceProvider)
        {
            var dispatcher = new CommandDispatcher();

            dispatcher.RegisterHandler<NewPostCommand>(serviceProvider.GetRequiredService<INewPostCommandHandler>().HandleAsync);
            dispatcher.RegisterHandler<LikePostCommand>(serviceProvider.GetRequiredService<ILikePostCommandHandler>().HandleAsync);
            dispatcher.RegisterHandler<AddCommentCommand>(serviceProvider.GetRequiredService<IAddCommentCommandHandler>().HandleAsync);
            dispatcher.RegisterHandler<EditMessageCommand>(serviceProvider.GetRequiredService<IEditMessageCommandHandler>().HandleAsync);
            dispatcher.RegisterHandler<EditCommentCommand>(serviceProvider.GetRequiredService<IEditCommentCommandHandler>().HandleAsync);
            dispatcher.RegisterHandler<RemoveCommentCommand>(serviceProvider.GetRequiredService<IRemoveCommentCommandHandler>().HandleAsync);
            dispatcher.RegisterHandler<DeletePostCommand>(serviceProvider.GetRequiredService<IDeletePostCommandHandler>().HandleAsync);

            services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

            
            return services;
        }
    }
}
